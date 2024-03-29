﻿using System.Security.Claims;
using FlowSheetAPI.DataTransferObjects;
using FlowSheetAPI.DomainModel;
using FlowSheetAPI.Repository.Interfaces;
using FlowSheetAPI.Services.Interfaces;
using Newtonsoft.Json;
using Response = FlowSheetAPI.DataTransferObjects.Response;

namespace FlowSheetAPI.Services.Implementation
{
    public class FlowsheetService : IFlowsheetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEhrUserService _ehrUserService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<FlowsheetService> _logger;

        public FlowsheetService(IUnitOfWork unitOfWork,
                                IEhrUserService ehrUserService,
                                ILogger<FlowsheetService> logger,
                                IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _ehrUserService = ehrUserService;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        // Get all the flowsheet records from the database.
        public async Task<FlowSheetWrapper> GetAllAsync()
        {
            var flowSheetWrapper = new FlowSheetWrapper();
            var list = Enumerable.Empty<Flowsheet>();
            list = await _unitOfWork.RegisterRepository<Flowsheet>().GetAllAsync(e => e.Doctor, e => e.Patient, e => e.SpecialityType, e => e.Approver);
            var columns = await Task.FromResult(_unitOfWork.RegisterRepository<FlowsheetTemplate>().Where(x => x.SpecialityType.SpecialityTypeId == list.FirstOrDefault().SpecialityType.SpecialityTypeId));

            flowSheetWrapper.SpecialityType = list.FirstOrDefault().SpecialityType;
            flowSheetWrapper.Flowsheets = ConvertFlowsheetToFlowSheetDM(list);
            flowSheetWrapper.FlowsheetColumns = GenerateFlowSheetColumns(columns.Result);

            return flowSheetWrapper;
        }

        // Get a flowsheet record by id from the database.
        public async Task<FlowSheetWrapper> GetByIdAsync(Guid id)
        {
            var flowSheetWrapper = new FlowSheetWrapper();
            var list = new List<Flowsheet>();
            list.Add(await Task.FromResult(_unitOfWork.RegisterRepository<Flowsheet>().Get(p => p.FlowsheetId == id, e => e.Doctor, e => e.Patient, e => e.SpecialityType, e => e.Approver)));
            var columns = await Task.FromResult(_unitOfWork.RegisterRepository<FlowsheetTemplate>().Where(x => x.SpecialityType.SpecialityTypeId == list.FirstOrDefault().SpecialityType.SpecialityTypeId));

            flowSheetWrapper.SpecialityType = list.FirstOrDefault().SpecialityType;
            flowSheetWrapper.Flowsheets = ConvertFlowsheetToFlowSheetDM(list);
            flowSheetWrapper.FlowsheetColumns = GenerateFlowSheetColumns(columns.Result);

            return flowSheetWrapper;
        }

        // Get a flowsheet record by patient ehr user name from the database.
        public async Task<FlowSheetWrapper> GetByPatient(int ehrPatientId)
        {
            var flowSheetWrapper = new FlowSheetWrapper();
            var list = Enumerable.Empty<Flowsheet>();
            var patient = await Task.FromResult(_unitOfWork.RegisterRepository<Patient>().Where(p => p.EhrPatientId == ehrPatientId).Result.FirstOrDefault());

            if (patient != null)
            {
                list = await Task.FromResult(_unitOfWork.RegisterRepository<Flowsheet>().GetAll(w => w.Patient.PatientId == patient.PatientId, e => e.Doctor, e => e.SpecialityType, e => e.Approver));
                var columns = await Task.FromResult(_unitOfWork.RegisterRepository<FlowsheetTemplate>().Where(x => x.SpecialityType.SpecialityTypeId == list.FirstOrDefault().SpecialityType.SpecialityTypeId));

                flowSheetWrapper.SpecialityType = list.FirstOrDefault().SpecialityType;
                flowSheetWrapper.Flowsheets = ConvertFlowsheetToFlowSheetDM(list);
                flowSheetWrapper.FlowsheetColumns = GenerateFlowSheetColumns(columns.Result);
            }

            return flowSheetWrapper;
        }

        public Response InsertFlowSheet(FlowSheetIM? inputModel)
        {
            var flowsheet = new Flowsheet();

            if (inputModel.FlowsheetId != Guid.Empty)
            {
                flowsheet = _unitOfWork.RegisterRepository<Flowsheet>().Get(p => p.FlowsheetId == inputModel.FlowsheetId, e => e.Doctor, e => e.Patient, e => e.SpecialityType, e => e.Approver);
            }

            var response = new Response();
            try
            {
                // Get the current logged in user id
                var loggedInUser = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                                   "System";

                // lookup by doctor ehr user name
                var doctor = _ehrUserService.GetDoctorByUserName(inputModel.EhrDoctorUserName).Result;

                // if doctor is null, create a new doctor
                if (doctor == null)
                {
                    // create a new doctor
                    var newDoctor = new Doctor
                    {
                        DoctorId = Guid.NewGuid(),
                        EhrUserName = inputModel.EhrDoctorUserName,
                        CreatedBy = loggedInUser,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedBy = loggedInUser,
                        UpdatedDate = DateTime.UtcNow
                    };
                    flowsheet.Doctor = newDoctor;
                }
                else
                {
                    flowsheet.Doctor = doctor;
                }

                // lookup by patient ehr user name
                var patient = _ehrUserService.GetPatientByUserName(inputModel.EhrPatientId).Result;

                // if patient is null, create a new patient
                if (patient == null)
                {
                    // create a new patient
                    var newPatient = new Patient
                    {
                        PatientId = Guid.NewGuid(),
                        EhrPatientId = inputModel.EhrPatientId,
                        CreatedBy = loggedInUser,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedBy = loggedInUser,
                        UpdatedDate = DateTime.UtcNow
                    };
                    flowsheet.Patient = newPatient;
                }
                else
                {
                    flowsheet.Patient = patient;
                }

                // Get SpecialityType from the look up
                var specialityType = _unitOfWork.RegisterRepository<SpecialityType>().GetByIdAsync(inputModel.SpecialityTypeId).Result;

                // Get SpecialityConditionType from the look up
                var specialityConditionType = _unitOfWork.RegisterRepository<SpecialityConditionType>().GetByIdAsync(inputModel.SpecialityConditionTypeId).Result;

                if (specialityType == null)
                {
                    response.Success = false;
                    response.Message = "Speciality Type not found.";
                    _logger.LogError("Speciality Type not found. " + flowsheet.SpecialityType.SpecialityTypeId + ". ");

                    return response;
                }

                if (specialityConditionType == null)
                {
                    response.Success = false;
                    response.Message = "Speciality Condition Type not found.";
                    _logger.LogError("Speciality Condition Type not found. " + flowsheet.SpecialityConditionType.SpecialityConditionTypeId + ". ");

                    return response;
                }

                //FlowSheet Note
                flowsheet.flowsheetNote = inputModel.Note;

                // Set the speciality type
                flowsheet.SpecialityType = specialityType;

                // Set the speciality condition type
                flowsheet.SpecialityConditionType = specialityConditionType;

                // Begin a transaction.
                _unitOfWork.BeginTransaction();

                //Insert or Update the Doctor record in the respective table
                _unitOfWork.RegisterRepository<Doctor>().UpsertAsync(flowsheet.Doctor);

                //Insert or Update the Patient record in the respective table
                _unitOfWork.RegisterRepository<Patient>().UpsertAsync(flowsheet.Patient);

                // Add flowsheet to the database.
                _unitOfWork.RegisterRepository<Flowsheet>().UpsertAsync(flowsheet);

                // Add flowsheet history to the database.
                _unitOfWork.RegisterRepository<FlowsheetHistory>().UpsertAsync(new FlowsheetHistory
                {
                    FlowsheetHistoryId = Guid.NewGuid(),
                    FlowsheetNote = flowsheet.flowsheetNote,
                    SpecialityType = flowsheet.SpecialityType,
                    SpecialityConditionType = flowsheet.SpecialityConditionType,
                    Flowsheet = flowsheet,
                    Patient = flowsheet.Patient,
                    Doctor = flowsheet.Doctor,
                });

                if (inputModel.FlowsheetId != Guid.Empty)
                {
                    // Add Flowsheet approver to the database
                    if (inputModel.ApproverId != Guid.Empty)
                    {
                        var approver = _unitOfWork.RegisterRepository<FlowsheetApprover>().GetByIdAsync(inputModel.ApproverId).Result;
                        _unitOfWork.RegisterRepository<FlowsheetApprovalHistory>().UpsertAsync(new FlowsheetApprovalHistory
                        {
                            FlowsheetApprovalHistoryId = Guid.NewGuid(),
                            Flowsheet = flowsheet,
                            FlowsheetApprover = approver
                        });
                    }
                }

                // Save the changes to the database.
                _unitOfWork.SaveChanges();

                // Commit the transaction.
                _unitOfWork.CommitTransaction();

                response.Success = true;
                response.Message = "Data successfully saved.";
                response.Cachekey = string.Concat("flowSheetWrapper", flowsheet.SpecialityConditionType.ConditionName.Replace(" ", ""), flowsheet.Patient.PatientId.ToString());                _logger.LogInformation("Flowsheet data successfully saved. " + flowsheet.FlowsheetId + ". ");
                _logger.LogInformation("Flowsheet data successfully saved. " + flowsheet.FlowsheetId + ". ");
            }
            catch (Exception ex)
            {
                // Rollback the transaction.
                _unitOfWork.RollbackTransaction();
                response.Success = false;
                response.Message = ex.Message;
                _logger.LogError("Error occurred while saving flowsheet data. " + ex);

                return response;
            }

            return response;
        }

        public async Task<FlowSheetWrapper> GetByDoctor(string ehrUserName)
        {
            var flowSheetWrapper = new FlowSheetWrapper();
            var list = Enumerable.Empty<Flowsheet>();
            var doctor = await Task.FromResult(_unitOfWork.RegisterRepository<Doctor>().Where(p => p.EhrUserName == ehrUserName).Result.FirstOrDefault());

            if (doctor == null) return flowSheetWrapper;

            list = await Task.FromResult(_unitOfWork.RegisterRepository<Flowsheet>().GetAll(w => w.Doctor.DoctorId == doctor.DoctorId, e => e.Patient, ehrUserName => ehrUserName.SpecialityType, e => e.Approver));
            var columns = await Task.FromResult(_unitOfWork.RegisterRepository<FlowsheetTemplate>().Where(x => x.SpecialityType.SpecialityTypeId == list.FirstOrDefault().SpecialityType.SpecialityTypeId));

            var flowsheets = list.ToList();
            flowSheetWrapper.SpecialityType = flowsheets.FirstOrDefault().SpecialityType;
            flowSheetWrapper.Flowsheets = ConvertFlowsheetToFlowSheetDM(flowsheets);
            flowSheetWrapper.FlowsheetColumns = GenerateFlowSheetColumns(columns.Result);

            return flowSheetWrapper;
        }

        public async Task<FlowSheetWrapper> GetByDoctorAndPatient(string ehrDoctorUserName, int ehrPatientId)
        {
            var flowSheetWrapper = new FlowSheetWrapper();
            var list = Enumerable.Empty<Flowsheet>();
            var patient = await Task.FromResult(_unitOfWork.RegisterRepository<Patient>().Where(p => p.EhrPatientId == ehrPatientId).Result.FirstOrDefault());

            var doctor = await Task.FromResult(_unitOfWork.RegisterRepository<Doctor>().Where(p => p.EhrUserName == ehrDoctorUserName).Result.FirstOrDefault());

            if (patient == null || doctor == null) return flowSheetWrapper;
            list = await Task.FromResult(_unitOfWork.RegisterRepository<Flowsheet>().GetAll(w => w.Patient.PatientId == patient.PatientId && w.Doctor.DoctorId == doctor.DoctorId, e => e.Doctor, e => e.Patient, e => e.SpecialityType, e => e.Approver));
            var columns = await Task.FromResult(_unitOfWork.RegisterRepository<FlowsheetTemplate>().Where(x => x.SpecialityType.SpecialityTypeId == list.FirstOrDefault().SpecialityType.SpecialityTypeId));

            flowSheetWrapper.SpecialityType = list.FirstOrDefault().SpecialityType;
            flowSheetWrapper.Flowsheets = ConvertFlowsheetToFlowSheetDM(list);
            flowSheetWrapper.FlowsheetColumns = GenerateFlowSheetColumns(columns.Result);

            return flowSheetWrapper;
        }

        public async Task<FlowSheetWrapper> GetBySpecialityConditionAndPatient(string conditionSpecialityType, int ehrPatientId)
        {
            var flowSheetWrapper = new FlowSheetWrapper();
            var patient = await Task.FromResult(_unitOfWork.RegisterRepository<Patient>().Where(p => p.EhrPatientId == ehrPatientId).Result.FirstOrDefault());

            if (patient == null) return flowSheetWrapper;

            var list = await Task.FromResult(_unitOfWork.RegisterRepository<Flowsheet>().GetAll(w => w.Patient.PatientId == patient.PatientId && w.SpecialityConditionType.ConditionName == conditionSpecialityType, e => e.Doctor, e => e.Patient, e => e.SpecialityType, e => e.Approver, e => e.SpecialityConditionType));
            var flowsheets = list.ToList();

            var specialityConditionType = await Task.FromResult(_unitOfWork.RegisterRepository<SpecialityConditionType>().GetAll(x => x.ConditionName == conditionSpecialityType, i => i.SpecialityType).FirstOrDefault());
            var specialityType = specialityConditionType.SpecialityType;
            flowSheetWrapper.SpecialityType = specialityType;
            flowSheetWrapper.SpecialityConditionType = specialityConditionType;

            if (flowsheets.Count == 0)
            {
                flowSheetWrapper.Flowsheets = new List<FlowSheetVM>();
            }

            var columns = await Task.FromResult(_unitOfWork.RegisterRepository<FlowsheetTemplate>().Where(x => x.SpecialityConditionType.ConditionName == conditionSpecialityType));
            flowSheetWrapper.Flowsheets = ConvertFlowsheetToFlowSheetDM(flowsheets);
            flowSheetWrapper.FlowsheetColumns = GenerateFlowSheetColumns(columns.Result);
            var approver = await Task.FromResult(_unitOfWork.RegisterRepository<FlowsheetApprover>().Where(x => x.SpecialityConditionType.ConditionName == conditionSpecialityType));

            var approverList = approver.Result.Select(item => new Approver
            {
                FlowsheetApproverId = item.FlowsheetApproverId,
                FirstName = item.FirstName,
                MiddleName = item.MiddleName,
                LastName = item.LastName,
                Initial = item.Initial,
                Designation = item.Designation,
                Telephone = item.Telephone,
                Fax = item.Fax,
                Address = item.Address,
                ClientId = item.ClientId,
                ClientName = item.ClientName
            })
                .ToList();

            // Add approver to the flowSheetWrapper
            flowSheetWrapper.Approver = approverList;

            return flowSheetWrapper;
        }

        public Response Upsert(Flowsheet? flowsheet)
        {
            var response = new Response();
            try
            {
                // Get the current logged in user id
                var loggedInUser = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                                   "System";

                // Get SpecialityType from the look up
                var specialityType = _unitOfWork.RegisterRepository<SpecialityType>().GetByIdAsync(flowsheet.SpecialityType.SpecialityTypeId).Result;

                // Get SpecialityConditionType from the look up
                var specialityConditionType = _unitOfWork.RegisterRepository<SpecialityConditionType>().GetByIdAsync(flowsheet.SpecialityConditionType.SpecialityConditionTypeId).Result;

                if (specialityType == null)
                {
                    response.Success = false;
                    response.Message = "Speciality Type not found.";
                    _logger.LogError("Speciality Type not found. " + flowsheet.SpecialityType.SpecialityTypeId + ". ");

                    return response;
                }

                if (specialityConditionType == null)
                {
                    response.Success = false;
                    response.Message = "Speciality Condition Type not found.";
                    _logger.LogError("Speciality Condition Type not found. " + flowsheet.SpecialityConditionType.SpecialityConditionTypeId + ". ");

                    return response;
                }

                // Set the speciality type
                flowsheet.SpecialityType = specialityType;

                // Set the speciality condition type
                flowsheet.SpecialityConditionType = specialityConditionType;

                // Begin a transaction.
                _unitOfWork.BeginTransaction();

                // lookup by doctor ehr user name
                var doctor = _ehrUserService.GetDoctorByUserName(flowsheet.Doctor.EhrUserName).Result;

                // if doctor is null, create a new doctor
                if (doctor == null)
                {
                    // create a new doctor
                    var newDoctor = new Doctor
                    {
                        DoctorId = Guid.NewGuid(),
                        EhrUserName = flowsheet.Doctor.EhrUserName,
                        CreatedBy = loggedInUser,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedBy = loggedInUser,
                        UpdatedDate = DateTime.UtcNow
                    };
                    flowsheet.Doctor = newDoctor;
                }
                else
                {
                    flowsheet.Doctor = doctor;
                }

                // lookup by patient ehr user name
                var patient = _ehrUserService.GetPatientByUserName(flowsheet.Patient.EhrPatientId).Result;

                // if patient is null, create a new patient
                if (patient == null)
                {
                    // create a new patient
                    var newPatient = new Patient
                    {
                        PatientId = Guid.NewGuid(),
                        EhrPatientId = flowsheet.Patient.EhrPatientId,
                        CreatedBy = loggedInUser,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedBy = loggedInUser,
                        UpdatedDate = DateTime.UtcNow
                    };
                    flowsheet.Patient = newPatient;
                }
                else
                {
                    flowsheet.Patient = patient;
                }

                //Insert or Update the Doctor record in the respective table
                _unitOfWork.RegisterRepository<Doctor>().UpsertAsync(flowsheet.Doctor);

                //Insert or Update the Patient record in the respective table
                _unitOfWork.RegisterRepository<Patient>().UpsertAsync(flowsheet.Patient);

                // Add flowsheet to the database.
                _unitOfWork.RegisterRepository<Flowsheet>().UpsertAsync(flowsheet);

                // Add flowsheet history to the database.
                _unitOfWork.RegisterRepository<FlowsheetHistory>().UpsertAsync(new FlowsheetHistory
                {
                    FlowsheetHistoryId = Guid.NewGuid(),
                    FlowsheetNote = flowsheet.flowsheetNote,
                    SpecialityType = flowsheet.SpecialityType,
                    SpecialityConditionType = flowsheet.SpecialityConditionType,
                    Flowsheet = flowsheet,
                    Patient = flowsheet.Patient,
                    Doctor = flowsheet.Doctor,
                });

                // Add Flowsheet approver to the database
                if (flowsheet.Approver != null)
                {
                    flowsheet.Approver.SpecialityType = flowsheet.SpecialityType;
                    flowsheet.Approver.SpecialityConditionType = flowsheet.SpecialityConditionType; _unitOfWork.RegisterRepository<FlowsheetApprover>().UpsertAsync(flowsheet.Approver);

                    _unitOfWork.RegisterRepository<FlowsheetApprovalHistory>().UpsertAsync(new FlowsheetApprovalHistory
                    {
                        FlowsheetApprovalHistoryId = Guid.NewGuid(),
                        Flowsheet = flowsheet,
                        FlowsheetApprover = flowsheet.Approver
                    });
                }

                // Save the changes to the database.
                _unitOfWork.SaveChanges();

                // Commit the transaction.
                _unitOfWork.CommitTransaction();

                response.Success = true;
                response.Message = "Data successfully saved.";
                _logger.LogInformation("Flowsheet data successfully saved. " + flowsheet.FlowsheetId + ". ");
            }
            catch (Exception ex)
            {
                // Rollback the transaction.
                _unitOfWork.RollbackTransaction();
                response.Success = false;
                response.Message = ex.Message;
                _logger.LogError("Error occurred while saving flowsheet data. " + ex);

                return response;
            }

            return response;
        }

        private static IEnumerable<FlowSheetVM> ConvertFlowsheetToFlowSheetDM(IEnumerable<Flowsheet> list)
        {
            var flowSheetlist = new List<FlowSheetVM>();
            var flowsheets = list.ToList();

            if (!flowsheets.Any()) return flowSheetlist;
            foreach (var item in flowsheets)
            {
                var flowsheetVm = new FlowSheetVM
                {
                    FlowsheetId = item.FlowsheetId,
                    CreatedBy = item.CreatedBy,
                    UpdatedBy = item.UpdatedBy,
                    CreatedDate = item.CreatedDate,
                    UpdatedDate = item.UpdatedDate,
                    Patient = item.Patient,
                    Doctor = item.Doctor,
                    SpecialityType = item.SpecialityType,
                    Approver = item.Approver
                };

                if (item.flowsheetNote != null)
                {
                    flowsheetVm.flowsheetNote = JsonConvert.DeserializeObject<FlowSheetNote>(item.flowsheetNote);
                }
                flowSheetlist.Add(flowsheetVm);
            }
            return flowSheetlist;
        }

        private static IEnumerable<FlowSheetColumns> GenerateFlowSheetColumns(IEnumerable<FlowsheetTemplate>? templateList)
        {
            return templateList.Select(col => new FlowSheetColumns(col.ColumnId, col.ColumnName, col.ColumnDisplayOrder)).ToList();
        }
    }
}
