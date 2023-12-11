using FlowSheetAPI.DomainModel;

namespace FlowSheetAPI.Services.Interfaces
{
    public interface ILookupService
    {
        Task<IEnumerable<SpecialityType?>> GetAllAsyncSpecialityTypes();
    }
}
