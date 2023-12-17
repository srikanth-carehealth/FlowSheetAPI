using System.Security.Claims;
using FlowSheetAPI.DataTransferObjects;
using FlowSheetAPI.DomainModel;
using FlowSheetAPI.Repository.Interfaces;
using FlowSheetAPI.Services.Interfaces;

namespace FlowSheetAPI.Services.Implementation
{
    public class AdminService: IAdminService    
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


            if (specialityConditionType == null)
            {
                response.Message = "Speciality Condition Type is null";
                response.Success = false;
                return response;
            }

            try
            {
                // Begin a transaction.
                _unitOfWork.BeginTransaction();
                
                // Upsert the speciality condition type.
                _unitOfWork.RegisterRepository<SpecialityConditionType>().UpsertAsync(specialityConditionType);                
                
                // Save the changes to the database.
                _unitOfWork.SaveChanges();
                
                // Commit the transaction.
                _unitOfWork.CommitTransaction();

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
            throw new NotImplementedException();
        }

        public Response Upsert(LabItem? labItem)
        {
            throw new NotImplementedException();
        }

        public Response Upsert(LabItemSpeciality? labItemSpeciality)
        {
            throw new NotImplementedException();
        }
    }
}
