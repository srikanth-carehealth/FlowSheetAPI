using FlowSheetAPI.DomainModel.Endocrinology;
using FlowSheetAPI.Repository.Interfaces;
using FlowSheetAPI.Services.Interfaces;
using System.Linq;

namespace FlowSheetAPI.Services.Implementation
{
    public class EndocrinologyService : IEndocrinologyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EndocrinologyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<Endocrinology>> GetAllAsync()
        {
            return _unitOfWork.RegisterRepository<Endocrinology>().GetAllAsync();
        }

        public Task<Endocrinology?> GetByIdAsync(Guid id)
        {
            return _unitOfWork.RegisterRepository<Endocrinology>().GetByIdAsync(id);
        }

        public async Task<IEnumerable<Endocrinology?>> GetByPatient(string patientId)
        {
            return await _unitOfWork.RegisterRepository<Endocrinology>().Where(w => w.PatientId == patientId);
        }

        public void Upsert(Endocrinology endocrinology)
        {
            try
            {
                // Begin a transaction.
                _unitOfWork.BeginTransaction();

                // Add the entity to the database.
                _unitOfWork.RegisterRepository<Endocrinology>().AddAsync(endocrinology);

                // Perform other operations that may be part of the same transaction...

                // Save the changes to the database.
                _unitOfWork.SaveChanges();

                // Commit the transaction.
                _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                // Rollback the transaction.
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }
    }
}
