using FlowSheetAPI.DomainModel.Endocrinology;

namespace FlowSheetAPI.Services.Interfaces
{
    public interface IEndocrinologyService
    {
        public Task<IEnumerable<Endocrinology>> GetAllAsync();
        public Task<Endocrinology?> GetByIdAsync(Guid id);
        public Task<IEnumerable<Endocrinology?>> GetByPatient(string patientId);
        public void Upsert(Endocrinology endocrinology);
    }
}
