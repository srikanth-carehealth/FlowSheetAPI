using FlowSheetAPI.DomainModel;
using FlowSheetAPI.Repository.Interfaces;
using FlowSheetAPI.Services.Interfaces;

namespace FlowSheetAPI.Services.Implementation
{
    public class LookupService : ILookupService
    {
        private readonly ILookupRepository<SpecialityType> _lookupSpecialityTypeRepository;

        public LookupService(ILookupRepository<SpecialityType> lookupSpecialityTypeRepository)
        {
            _lookupSpecialityTypeRepository = lookupSpecialityTypeRepository;
        }

        public async Task<IEnumerable<SpecialityType?>> GetAllAsyncSpecialityTypes()
            => await _lookupSpecialityTypeRepository.GetAllAsync();

    }
}
