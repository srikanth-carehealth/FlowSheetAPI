using Microsoft.EntityFrameworkCore.Storage;

namespace FlowSheetAPI.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> RegisterRepository<TEntity>() where TEntity : class;
        void SaveChanges();
        IDbContextTransaction BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
