using System.Security.Claims;
using FlowSheetAPI.DataTransferObjects;
using FlowSheetAPI.DomainModel;
using FlowSheetAPI.Model;
using FlowSheetAPI.Repository.Interfaces;
using FlowSheetAPI.Services.Interfaces;

namespace FlowSheetAPI.Services.Implementation
{
    public class AdminService : IAdminService
    {
        public ILogger<AdminService> _logger { get; }
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminService(ILogger<AdminService> logger,
                            IUnitOfWork unitOfWork,
                            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<IEnumerable<SpecialityConditionType>> GetSpecialityConditionTypes()
        {
            return _unitOfWork.RegisterRepository<SpecialityConditionType>().GetAllAsync(i => i.SpecialityType);
        }

        public Task<IEnumerable<SpecialityConditionType>> GetConditionTypeBySpeciality(Guid specialityTypeId)
        {
            return _unitOfWork.RegisterRepository<SpecialityConditionType>().Where(x => x.SpecialityType.SpecialityTypeId == specialityTypeId);
        }

        public Task<IEnumerable<SpecialityType>> GetSpecialityTypes()
        {
            return _unitOfWork.RegisterRepository<SpecialityType>().GetAllAsync();
        }

        public Task<IEnumerable<LabItem>> GetLabItems()
        {
            return _unitOfWork.RegisterRepository<LabItem>().GetAllAsync();
        }

        public Task<IEnumerable<LabItemSpeciality>> GetLabItemSpeciality()
        {
            return _unitOfWork.RegisterRepository<LabItemSpeciality>().GetAllAsync(i => i.SpecialityType, i => i.SpecialityConditionType, i => i.LabItem);
        }

        public Task<IEnumerable<LabItemSpeciality>> GetLabItemBySpeciality(Guid specialityTypeId)
        {
            return _unitOfWork.RegisterRepository<LabItemSpeciality>().Where(x => x.SpecialityType.SpecialityTypeId == specialityTypeId);
        }

        public async Task<SpecialityType?> GetSpecialityTypeById(Guid specialityTypeId)
        {
            return await _unitOfWork.RegisterRepository<SpecialityType>().GetByIdAsync(specialityTypeId);
        }

        public async Task<LabItem?> GetLabItemById(Guid labItemId)
        {
            return await _unitOfWork.RegisterRepository<LabItem>().GetByIdAsync(labItemId);
        }

        public Task<IEnumerable<FlowsheetTemplate>> GetFlowsheetTemplates()
        {
            return _unitOfWork.RegisterRepository<FlowsheetTemplate>().GetAllAsync(i => i.SpecialityType, i => i.SpecialityConditionType);
        }

        public Task<IEnumerable<FlowsheetApprover>> GetFlowsheetApprovers()
        {
            return _unitOfWork.RegisterRepository<FlowsheetApprover>().GetAllAsync(i => i.SpecialityType, i => i.SpecialityConditionType);
        }

        public Response Upsert(SpecialityConditionTypeViewModel specialityConditionTypeViewModel)
        {
            var response = new Response();

            // Get the current logged in user id
            var loggedInUser = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                               "System";
            try
            {
                // Begin a transaction.
                _unitOfWork.BeginTransaction();

                if (specialityConditionTypeViewModel?.SpecialityTypeId != null)
                {
                    // Get the speciality type.
                    var specialityType = _unitOfWork.RegisterRepository<SpecialityType>().GetByIdAsync(specialityConditionTypeViewModel.SpecialityTypeId).Result;

                    if (specialityConditionTypeViewModel.SpecialityConditionTypeId == Guid.Empty)
                    {
                        var newSpecialityConditionType = new SpecialityConditionType
                        {
                            ClientId = specialityConditionTypeViewModel.ClientId,
                            ClientName = specialityConditionTypeViewModel.ClientName,
                            ConditionName = specialityConditionTypeViewModel.ConditionName,
                            SpecilityConditionCode = specialityConditionTypeViewModel.SpecilityConditionCode,
                            IsActive = true,
                            SpecialityType = specialityType,
                            CreatedBy = loggedInUser,
                            CreatedDate = DateTime.UtcNow,
                            UpdatedBy = loggedInUser,
                            UpdatedDate = DateTime.UtcNow
                        };

                        // Upsert the speciality condition type.
                        _unitOfWork.RegisterRepository<SpecialityConditionType>().UpsertAsync(newSpecialityConditionType);
                    }
                    else
                    {
                        // Get the speciality condition type.
                        var specialityConditionTypeData = _unitOfWork.RegisterRepository<SpecialityConditionType>().GetByIdAsync(specialityConditionTypeViewModel.SpecialityConditionTypeId).Result;

                        // Update the speciality condition type.
                        if (specialityConditionTypeData != null)
                        {
                            specialityConditionTypeData.ClientId = specialityConditionTypeViewModel.ClientId;
                            specialityConditionTypeData.ClientName = specialityConditionTypeViewModel.ClientName;
                            specialityConditionTypeData.ConditionName = specialityConditionTypeViewModel.ConditionName;
                            specialityConditionTypeData.SpecilityConditionCode = specialityConditionTypeViewModel.SpecilityConditionCode;
                            specialityConditionTypeData.IsActive = specialityConditionTypeViewModel.IsActive;
                            specialityConditionTypeData.SpecialityType = specialityType;

                            // Upsert the speciality condition type.
                            _unitOfWork.RegisterRepository<SpecialityConditionType>().UpsertAsync(specialityConditionTypeData);
                        }
                    }

                    // Save the changes to the database.
                    _unitOfWork.SaveChanges();

                    // Commit the transaction.
                    _unitOfWork.CommitTransaction();

                    response.Success = true;
                    response.Message = "Speciality condition data saved successfully.";

                    return response;
                }
                response.Success = false;
                response.Message = "Speciality type data cannot be null.";
                return response;
            }
            catch (Exception e)
            {
                _unitOfWork.RollbackTransaction();
                _logger.LogError("Error occurred while saving speciality condition data. " + e);
                response.Success = false;
                response.Message = "Error occurred while saving speciality condition data.";
                return response;
            }
        }

        public Response Upsert(SpecialityTypeViewModel? specialityTypeViewModel)
        {
            var response = new Response();

            // Get the current logged in user id
            var loggedInUser = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                               "System";
            try
            {
                // Begin a transaction.
                _unitOfWork.BeginTransaction();

                if (specialityTypeViewModel != null)
                {
                    if (specialityTypeViewModel.SpecialityTypeId == Guid.Empty)
                    {
                        var newSpecialityType = new SpecialityType
                        {
                            ClientId = specialityTypeViewModel.ClientId,
                            ClientName = specialityTypeViewModel.ClientName,
                            SpecialityCode = specialityTypeViewModel.SpecialityCode,
                            SpecialityName = specialityTypeViewModel.SpecialityName,
                            TotalApprovalCount = specialityTypeViewModel.TotalApprovalCount,
                            IsActive = true,
                            CreatedBy = loggedInUser,
                            CreatedDate = DateTime.UtcNow,
                            UpdatedBy = loggedInUser,
                            UpdatedDate = DateTime.UtcNow
                        };

                        // Upsert the speciality type.
                        _unitOfWork.RegisterRepository<SpecialityType>().UpsertAsync(newSpecialityType);
                    }
                    else
                    {
                        // Get the speciality type.
                        var specialityType = _unitOfWork.RegisterRepository<SpecialityType>().GetByIdAsync(specialityTypeViewModel.SpecialityTypeId).Result;

                        // Update the speciality type.
                        if (specialityType != null)
                        {
                            specialityType.ClientId = specialityTypeViewModel.ClientId;
                            specialityType.ClientName = specialityTypeViewModel.ClientName;
                            specialityType.SpecialityCode = specialityTypeViewModel.SpecialityCode;
                            specialityType.SpecialityName = specialityTypeViewModel.SpecialityName;
                            specialityType.TotalApprovalCount = specialityTypeViewModel.TotalApprovalCount;
                            specialityType.IsActive = specialityTypeViewModel.IsActive;

                            // Upsert the speciality type.
                            _unitOfWork.RegisterRepository<SpecialityType>().UpsertAsync(specialityType);
                        }
                    }

                    // Save the changes to the database.
                    _unitOfWork.SaveChanges();

                    // Commit the transaction.
                    _unitOfWork.CommitTransaction();

                    response.Success = true;
                    response.Message = "Speciality data saved successfully.";

                    return response;
                }

                response.Success = false;
                response.Message = "Speciality type data cannot be null.";
                return response;
            }
            catch (Exception e)
            {
                _unitOfWork.RollbackTransaction();
                _logger.LogError("Error occurred while saving Speciality item data. " + e);
                var errorResponse = new Response { Success = false, Message = "Error occurred while saving Speciality item data." };
                return errorResponse;
            }
        }

        public Response Upsert(LabItemViewModel? labItemViewModel)
        {
            try
            {
                var response = new Response();

                // Get the current logged in user id
                var loggedInUser = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                                   "System";

                // Begin a transaction.
                _unitOfWork.BeginTransaction();
                if (labItemViewModel != null)
                {
                    if (labItemViewModel.LabItemId == Guid.Empty)
                    {
                        var newLabItem = new LabItem
                        {
                            LabItemName = labItemViewModel.LabItemName,
                            LabItemCode = labItemViewModel.LabItemCode,
                            IsActive = true,
                            CreatedBy = loggedInUser,
                            CreatedDate = DateTime.UtcNow,
                            UpdatedBy = loggedInUser,
                            UpdatedDate = DateTime.UtcNow
                        };

                        // Upsert the lab item.
                        _unitOfWork.RegisterRepository<LabItem>().UpsertAsync(newLabItem);
                    }
                    else
                    {
                        // Get the lab item.
                        var labItem = _unitOfWork.RegisterRepository<LabItem>().GetByIdAsync(labItemViewModel.LabItemId).Result;

                        // Update the lab item.
                        if (labItem != null)
                        {
                            labItem.LabItemName = labItemViewModel.LabItemName;
                            labItem.LabItemCode = labItemViewModel.LabItemCode;
                            labItem.IsActive = labItemViewModel.IsActive;

                            // Upsert the lab item.
                            _unitOfWork.RegisterRepository<LabItem>().UpsertAsync(labItem);
                        }
                    }

                    // Save the changes to the database.
                    _unitOfWork.SaveChanges();

                    // Commit the transaction.
                    _unitOfWork.CommitTransaction();

                    response.Success = true;
                    response.Message = "Lab item data saved successfully.";
                    return response;
                }

                var errorResponse = new Response { Success = false, Message = "Lab item data cannot be null." };
                return errorResponse;
            }
            catch (Exception e)
            {
                _unitOfWork.RollbackTransaction();
                _logger.LogError("Error occurred while saving lab item data. " + e);
                var errorResponse = new Response { Success = false, Message = "Error occurred while saving lab item data." };
                return errorResponse;
            }
        }

        public Response Upsert(LabItemSpecialityViewModel? labItemSpecialityViewModel)
        {
            try
            {
                var response = new Response();

                // Get the current logged in user id
                var loggedInUser = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                                   "System";

                // Begin a transaction.
                _unitOfWork.BeginTransaction();

                if (labItemSpecialityViewModel != null)
                {

                    // Get the lab item.
                    var labItem = _unitOfWork.RegisterRepository<LabItem>().GetByIdAsync(labItemSpecialityViewModel.LabItemId).Result;

                    // Get the speciality type.
                    var specialityType = _unitOfWork.RegisterRepository<SpecialityType>().GetByIdAsync(labItemSpecialityViewModel.SpecialityTypeId).Result;

                    // Get the Speciality condition type.
                    var specialityConditionType = _unitOfWork.RegisterRepository<SpecialityConditionType>().GetByIdAsync(labItemSpecialityViewModel.SpecialityConditionTypeId).Result;

                    if (labItemSpecialityViewModel.LabItemSpecialityId == Guid.Empty)
                    {
                        var newLabItemSpeciality = new LabItemSpeciality
                        {
                            LabItemSpecialityId = Guid.NewGuid(),
                            LabItem = labItem,
                            SpecialityType = specialityType,
                            SpecialityConditionType = specialityConditionType,
                            ClientId = labItemSpecialityViewModel.ClientId,
                            ClientName = labItemSpecialityViewModel.ClientName,
                            IsActive = true,
                            CreatedBy = loggedInUser,
                            CreatedDate = DateTime.UtcNow,
                            UpdatedBy = loggedInUser,
                            UpdatedDate = DateTime.UtcNow
                        };

                        // Upsert the lab item speciality.
                        _unitOfWork.RegisterRepository<LabItemSpeciality>().UpsertAsync(newLabItemSpeciality);
                    }

                    else
                    {
                        // Get the lab item speciality.
                        var labItemSpecialityData = _unitOfWork.RegisterRepository<LabItemSpeciality>().GetByIdAsync(labItemSpecialityViewModel.LabItemSpecialityId).Result;

                        // Update the lab item speciality.
                        if (labItemSpecialityData != null)
                        {
                            labItemSpecialityData.LabItem = labItem;
                            labItemSpecialityData.SpecialityType = specialityType;
                            labItemSpecialityData.SpecialityConditionType = specialityConditionType;
                            labItemSpecialityData.ClientId = labItemSpecialityViewModel.ClientId;
                            labItemSpecialityData.ClientName = labItemSpecialityViewModel.ClientName;
                            labItemSpecialityData.IsActive = labItemSpecialityViewModel.IsActive;

                            // Upsert the lab item speciality.
                            _unitOfWork.RegisterRepository<LabItemSpeciality>().UpsertAsync(labItemSpecialityData);
                        }
                    }

                    // Save the changes to the database.
                    _unitOfWork.SaveChanges();

                    // Commit the transaction.
                    _unitOfWork.CommitTransaction();

                    response.Success = true;
                    response.Message = "Lab item speciality data saved successfully.";
                    return response;
                }

                var errorResponse = new Response
                { Success = false, Message = "Lab item speciality data cannot be null." };
                return errorResponse;
            }
            catch (Exception e)
            {
                _unitOfWork.RollbackTransaction();
                _logger.LogError("Error occurred while saving lab item speciality data. " + e);
                var errorResponse = new Response { Success = false, Message = "Error occurred while saving lab item speciality data." };
                return errorResponse;
            }
        }

        public Response Upsert(FlowsheetApproverViewModel? flowsheetApproverViewModel)
        {
            try
            {
                var response = new Response();

                // Get the current logged in user id
                var loggedInUser = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                                   "System";

                // Begin a transaction.
                _unitOfWork.BeginTransaction();

                if (flowsheetApproverViewModel != null)
                {
                    if (flowsheetApproverViewModel.FlowsheetApproverId == Guid.Empty)
                    {
                        var newFlowsheetApprover = new FlowsheetApprover
                        {
                            FlowsheetApproverId = Guid.NewGuid(),
                            FirstName = flowsheetApproverViewModel.FirstName,
                            MiddleName = flowsheetApproverViewModel.MiddleName,
                            LastName = flowsheetApproverViewModel.LastName,
                            Initial = flowsheetApproverViewModel.Initial,
                            Designation = flowsheetApproverViewModel.Designation,
                            Telephone = flowsheetApproverViewModel.Telephone,
                            Fax = flowsheetApproverViewModel.Fax,
                            Address = flowsheetApproverViewModel.Address,
                            IsActive = true,
                            CreatedBy = loggedInUser,
                            CreatedDate = DateTime.UtcNow,
                            UpdatedBy = loggedInUser,
                            UpdatedDate = DateTime.UtcNow
                        };

                        // Upsert the flowsheet approver.
                        _unitOfWork.RegisterRepository<FlowsheetApprover>().UpsertAsync(newFlowsheetApprover);
                    }
                    else
                    {
                        // Get the flowsheet approver.
                        var flowsheetApprover = _unitOfWork.RegisterRepository<FlowsheetApprover>()
                            .GetByIdAsync(flowsheetApproverViewModel.FlowsheetApproverId).Result;

                        // Update the flowsheet approver.
                        if (flowsheetApprover != null)
                        {
                            flowsheetApprover.FirstName = flowsheetApproverViewModel.FirstName;
                            flowsheetApprover.MiddleName = flowsheetApproverViewModel.MiddleName;
                            flowsheetApprover.LastName = flowsheetApproverViewModel.LastName;
                            flowsheetApprover.Initial = flowsheetApproverViewModel.Initial;
                            flowsheetApprover.Designation = flowsheetApproverViewModel.Designation;
                            flowsheetApprover.Telephone = flowsheetApproverViewModel.Telephone;
                            flowsheetApprover.Fax = flowsheetApproverViewModel.Fax;
                            flowsheetApprover.Address = flowsheetApproverViewModel.Address;
                            flowsheetApprover.IsActive = flowsheetApproverViewModel.IsActive;

                            // Upsert the flowsheet approver.
                            _unitOfWork.RegisterRepository<FlowsheetApprover>().UpsertAsync(flowsheetApprover);

                            // Save the changes to the database.
                            _unitOfWork.SaveChanges();

                            // Commit the transaction.
                            _unitOfWork.CommitTransaction();

                            response.Success = true;
                            response.Message = "Flowsheet approver data saved successfully.";

                            return response;
                        }
                    }
                }

                var errorResponse = new Response { Success = false, Message = "Flowsheet approver data cannot be null." };
                return errorResponse;
            }
            catch (Exception e)
            {
                _unitOfWork.RollbackTransaction();
                _logger.LogError("Error occurred while saving flowsheet approver data. " + e);
                var errorResponse = new Response { Success = false, Message = "Error occurred while saving flowsheet approver data." };
                return errorResponse;
            }
        }

        public Response Upsert(FlowsheetTemplateViewModel? flowSheetColumnViewModel)
        {
            try
            {
                var response = new Response();

                // Get the current logged in user id
                var loggedInUser = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                                   "System";

                // Begin a transaction.
                _unitOfWork.BeginTransaction();

                if (flowSheetColumnViewModel != null)
                {
                    var specialityType = _unitOfWork.RegisterRepository<SpecialityType>()
                        .GetByIdAsync(flowSheetColumnViewModel.SpecialityTypeId).Result;

                    var specialityConditionType = _unitOfWork.RegisterRepository<SpecialityConditionType>()
                        .GetByIdAsync(flowSheetColumnViewModel.SpecialityConditionTypeId).Result;


                    if (flowSheetColumnViewModel.FlowsheetTemplateId == Guid.Empty)
                    {
                        var newFlowsheetTemplate = new FlowsheetTemplate
                        {
                            FlowsheetTemplateId = Guid.NewGuid(),
                            ColumnName = flowSheetColumnViewModel.ColumnName,
                            ClientId = flowSheetColumnViewModel.ClientId,
                            ClientName = flowSheetColumnViewModel.ClientName,
                            IsActive = true,
                            SpecialityType = specialityType,
                            SpecialityConditionType = specialityConditionType,
                            CreatedBy = loggedInUser,
                            CreatedDate = DateTime.UtcNow,
                            UpdatedBy = loggedInUser,
                            UpdatedDate = DateTime.UtcNow
                        };

                        // Upsert the flowsheet template.
                        _unitOfWork.RegisterRepository<FlowsheetTemplate>().UpsertAsync(newFlowsheetTemplate);
                    }
                    else
                    {
                        // Get the flowsheet template.
                        var flowsheetTemplate = _unitOfWork.RegisterRepository<FlowsheetTemplate>()
                            .GetByIdAsync(flowSheetColumnViewModel.FlowsheetTemplateId).Result;

                        // Update the flowsheet template.
                        if (flowsheetTemplate != null)
                        {
                            flowsheetTemplate.ColumnName = flowSheetColumnViewModel.ColumnName;
                            flowsheetTemplate.ClientId = flowSheetColumnViewModel.ClientId;
                            flowsheetTemplate.ClientName = flowSheetColumnViewModel.ClientName;
                            flowsheetTemplate.IsActive = flowSheetColumnViewModel.IsActive;
                            flowsheetTemplate.SpecialityType = specialityType;
                            flowsheetTemplate.SpecialityConditionType = specialityConditionType;

                            // Upsert the flowsheet template.
                            _unitOfWork.RegisterRepository<FlowsheetTemplate>().UpsertAsync(flowsheetTemplate);
                        }
                    }

                    // Save the changes to the database.
                    _unitOfWork.SaveChanges();

                    // Commit the transaction.
                    _unitOfWork.CommitTransaction();

                    response.Success = true;
                    response.Message = "Flowsheet template data saved successfully.";

                    return response;
                }

                var errorResponse = new Response { Success = false, Message = "Flowsheet template data cannot be null." };
                return errorResponse;
            }
            catch (Exception e)
            {
                _unitOfWork.RollbackTransaction();
                _logger.LogError("Error occurred while saving flowsheet template data. " + e);
                var errorResponse = new Response { Success = false, Message = "Error occurred while saving flowsheet template data." };
                return errorResponse;
            }
        }
    }
}
