using System.Security.Claims;
using FlowSheetAPI.DomainModel;
using FlowSheetAPI.Repository.Interfaces;
using FlowSheetAPI.Services.Interfaces;
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
        public async Task<IEnumerable<Flowsheet>> GetAllAsync() => await _unitOfWork.RegisterRepository<Flowsheet>().GetAllAsync(e => e.Doctor, e => e.Patient, e => e.SpecialityType, e => e.Approver);

        // Get a flowsheet record by id from the database.
        public async Task<Flowsheet?> GetByIdAsync(Guid id)
        {
            return await Task.FromResult(_unitOfWork.RegisterRepository<Flowsheet>().Get(p => p.FlowsheetId == id, e => e.Doctor, e => e.Patient, e => e.SpecialityType, e => e.Approver));
        }

        // Get a flowsheet record by patient ehr user name from the database.
        public async Task<IEnumerable<Flowsheet?>> GetByPatient(string ehrUserName)
        {
            var list = Enumerable.Empty<Flowsheet>();
            var patient = await Task.FromResult(_unitOfWork.RegisterRepository<Patient>().Where(p => p.EhrUserName == ehrUserName).Result.FirstOrDefault());

            if (patient != null)
            {
                list = await Task.FromResult(_unitOfWork.RegisterRepository<Flowsheet>().GetAll(w => w.Patient.PatientId == patient.PatientId, e => e.Doctor, e => e.SpecialityType, e => e.Approver));
            }

            return list;
        }

        public async Task<IEnumerable<Flowsheet?>> GetByDoctor(string ehrUserName)
        {
            var list = Enumerable.Empty<Flowsheet>();
            var doctor = await Task.FromResult(_unitOfWork.RegisterRepository<Doctor>().Where(p => p.EhrUserName == ehrUserName).Result.FirstOrDefault());

            if (doctor != null)
            {
                list = await Task.FromResult(_unitOfWork.RegisterRepository<Flowsheet>().GetAll(w => w.Doctor.DoctorId == doctor.DoctorId, e => e.Patient, ehrUserName => ehrUserName.SpecialityType, e => e.Approver));
            }

            return list;
        }

        public async Task<IEnumerable<Flowsheet?>> GetByDoctorAndPatient(string ehrDoctorUserName, string ehrPatientUserName)
        {
            var list = Enumerable.Empty<Flowsheet>();
            var patient = await Task.FromResult(_unitOfWork.RegisterRepository<Patient>().Where(p => p.EhrUserName == ehrPatientUserName).Result.FirstOrDefault());

            var doctor = await Task.FromResult(_unitOfWork.RegisterRepository<Doctor>().Where(p => p.EhrUserName == ehrDoctorUserName).Result.FirstOrDefault());

            if (patient != null && doctor != null)
            {
                list = await Task.FromResult(_unitOfWork.RegisterRepository<Flowsheet>().GetAll(w => w.Patient.PatientId == patient.PatientId && w.Doctor.DoctorId == doctor.DoctorId, e => e.Doctor, e => e.Patient, e => e.SpecialityType, e => e.Approver));
            }

            return list;
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

                if (specialityType == null)
                {
                    response.Success = false;
                    response.Message = "Speciality Type not found.";
                    _logger.LogError("Speciality Type not found. " + flowsheet.SpecialityType.SpecialityTypeId + ". ");

                    return response;
                }

                flowsheet.SpecialityType = specialityType;

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
                var patient = _ehrUserService.GetPatientByUserName(flowsheet.Patient.EhrUserName).Result;

                // if patient is null, create a new patient
                if (patient == null)
                {
                    // create a new patient
                    var newPatient = new Patient
                    {
                        PatientId = Guid.NewGuid(),
                        EhrUserName = flowsheet.Patient.EhrUserName,
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
                    Flowsheet = flowsheet,
                    Patient = flowsheet.Patient,
                    Doctor = flowsheet.Doctor,
                });

                // Add Flowsheet approver to the database
                if (flowsheet.Approver != null)
                {
                    flowsheet.Approver.SpecialityType = flowsheet.SpecialityType;
                    _unitOfWork.RegisterRepository<FlowsheetApprover>().UpsertAsync(flowsheet.Approver);

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
    }
}
