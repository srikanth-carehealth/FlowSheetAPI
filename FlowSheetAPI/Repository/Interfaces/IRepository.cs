using System.Linq.Expressions;

namespace FlowSheetAPI.Repository.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity? GetById(Guid id);
        IQueryable<TEntity> GetAll();
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task SaveChangesAsync();
        Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate);
        TEntity Get(Expression<Func<TEntity?, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includeProperties);
    }
}
