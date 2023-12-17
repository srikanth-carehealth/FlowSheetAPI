using FlowSheetAPI.DataTransferObjects;
using FlowSheetAPI.DomainModel;

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
        public Response Upsert(SpecialityConditionType? specialityConditionType);
        public Response Upsert(SpecialityType? specialityType);
        public Response Upsert(LabItem? labItem);
        public Response Upsert(LabItemSpeciality? labItemSpeciality);
    }
}
