using FlowSheetAPI.DataTransferObjects;
using FlowSheetAPI.DomainModel;
using FlowSheetAPI.Model;

namespace FlowSheetAPI.Services.Interfaces
{
    public interface IAdminService
    {
        public Task<IEnumerable<SpecialityConditionType>> GetSpecialityConditionTypes();
        public Task<IEnumerable<SpecialityConditionType>> GetConditionTypeBySpeciality(Guid specialityTypeId);
        public Task<IEnumerable<SpecialityType>> GetSpecialityTypes();
        public Task<IEnumerable<LabItem>> GetLabItems();
        public Task<IEnumerable<LabItemSpeciality>> GetLabItemSpeciality();
        public Task<IEnumerable<LabItemSpeciality>> GetLabItemBySpeciality(Guid specialityTypeId);
        public Task<SpecialityType?> GetSpecialityTypeById(Guid specialityTypeId);
        public Task<LabItem?> GetLabItemById(Guid labItemId);
        public Response Upsert(SpecialityConditionTypeViewModel specialityConditionTypeViewModel);
        public Response Upsert(SpecialityTypeViewModel specialityTypeViewModel);
        public Response Upsert(LabItemViewModel? labItemViewModel);
        public Response Upsert(LabItemSpecialityViewModel? labItemSpecialityViewModel);
    }
}
