using System.Security.Claims;
using FlowSheetAPI.DataTransferObjects;
using FlowSheetAPI.DomainModel;
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
            return _unitOfWork.RegisterRepository<SpecialityConditionType>().GetAllAsync();
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
            return _unitOfWork.RegisterRepository<LabItemSpeciality>().GetAllAsync();
        }

        public Task<IEnumerable<LabItemSpeciality>> GetLabItemBySpeciality(Guid specialityTypeId)
        {
            return _unitOfWork.RegisterRepository<LabItemSpeciality>().Where(x => x.SpecialityType.SpecialityTypeId == specialityTypeId);
        }

        public Response Upsert(SpecialityConditionType? specialityConditionType)
        {
            var response = new Response();

            // Get the current logged in user id
            var loggedInUser = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                               "System";
            try
            {
                // Begin a transaction.
                _unitOfWork.BeginTransaction();

                if (specialityConditionType?.SpecialityType != null)
                {
                    // Get the speciality type.
                    var specialityType = _unitOfWork.RegisterRepository<SpecialityType>().GetByIdAsync(specialityConditionType.SpecialityType.SpecialityTypeId).Result;

                    if (specialityConditionType.SpecialityConditionTypeId == Guid.Empty)
                    {
                        var newSpecialityConditionType = new SpecialityConditionType
                        {
                            ClientId = specialityConditionType.ClientId,
                            ClientName = specialityConditionType.ClientName,
                            ConditionName = specialityConditionType.ConditionName,
                            SpecilityConditionCode = specialityConditionType.SpecilityConditionCode,
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

                    // Upsert the speciality condition type.
                    _unitOfWork.RegisterRepository<SpecialityConditionType>().UpsertAsync(specialityConditionType);

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

        public Response Upsert(SpecialityType? specialityType)
        {
            var response = new Response();

            // Get the current logged in user id
            var loggedInUser = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                               "System";
            try
            {
                // Begin a transaction.
                _unitOfWork.BeginTransaction();

                if (specialityType != null)
                {
                    if (specialityType.SpecialityTypeId == Guid.Empty)
                    {
                        var newSpecialityType = new SpecialityType
                        {
                            ClientId = specialityType.ClientId,
                            ClientName = specialityType.ClientName,
                            SpecialityCode = specialityType.SpecialityCode,
                            SpecialityName = specialityType.SpecialityName,
                            TotalApprovalCount = specialityType.TotalApprovalCount,
                            IsActive = true,
                            CreatedBy = loggedInUser,
                            CreatedDate = DateTime.UtcNow,
                            UpdatedBy = loggedInUser,
                            UpdatedDate = DateTime.UtcNow
                        };

                        // Upsert the speciality type.
                        _unitOfWork.RegisterRepository<SpecialityType>().UpsertAsync(newSpecialityType);
                    }

                    // Upsert the speciality type.
                    _unitOfWork.RegisterRepository<SpecialityType>().UpsertAsync(specialityType);

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
                return response;
            }
        }

        public Response Upsert(LabItem? labItem)
        {
            try
            {

                // Begin a transaction.
                _unitOfWork.BeginTransaction();
                if (labItem != null)
                {
                    if (labItem.LabItemId == Guid.Empty)
                    {
                        var newLabItem = new LabItem
                        {
                            LabItemName = labItem.LabItemName,
                            LabItemCode = labItem.LabItemCode,
                            IsActive = true,
                            CreatedBy = "System",
                            CreatedDate = DateTime.UtcNow,
                            UpdatedBy = "System",
                            UpdatedDate = DateTime.UtcNow
                        };

                        // Upsert the lab item.
                        _unitOfWork.RegisterRepository<LabItem>().UpsertAsync(newLabItem);
                    }

                    // Upsert the lab item.
                    _unitOfWork.RegisterRepository<LabItem>().UpsertAsync(labItem);

                    // Save the changes to the database.
                    _unitOfWork.SaveChanges();

                    // Commit the transaction.
                    _unitOfWork.CommitTransaction();

                    var response = new Response { Success = true, Message = "Lab item data saved successfully." };
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

        public Response Upsert(LabItemSpeciality? labItemSpeciality)
        {
            try
            {
                // Begin a transaction.
                _unitOfWork.BeginTransaction();

                if (labItemSpeciality != null)
                {
                    if (labItemSpeciality.LabItemSpecialityId == Guid.Empty)
                    {
                        var newLabItemSpeciality = new LabItemSpeciality
                        {
                            LabItemSpecialityId = Guid.NewGuid(),
                            LabItem = labItemSpeciality.LabItem,
                            SpecialityType = labItemSpeciality.SpecialityType,
                            ClientId = labItemSpeciality.ClientId,
                            ClientName = labItemSpeciality.ClientName,
                            IsActive = true,
                            CreatedBy = "System",
                            CreatedDate = DateTime.UtcNow,
                            UpdatedBy = "System",
                            UpdatedDate = DateTime.UtcNow
                        };

                        // Upsert the lab item speciality.
                        _unitOfWork.RegisterRepository<LabItemSpeciality>().UpsertAsync(newLabItemSpeciality);
                    }

                    // Upsert the lab item speciality.
                    _unitOfWork.RegisterRepository<LabItemSpeciality>().UpsertAsync(labItemSpeciality);

                    // Save the changes to the database.
                    _unitOfWork.SaveChanges();

                    // Commit the transaction.
                    _unitOfWork.CommitTransaction();

                    var response = new Response
                    { Success = true, Message = "Lab item speciality data saved successfully." };
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
    }
}
