using FlowSheetAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace FlowSheetAPI.Repository.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FlowSheetDbContext _context;
        private Dictionary<Type, object> _repositories;
        private IDbContextTransaction _transaction;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UnitOfWork(FlowSheetDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _repositories = new Dictionary<Type, object>();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public IRepository<TEntity> RegisterRepository<TEntity>() where TEntity : class
        {
            if (_repositories.ContainsKey(typeof(TEntity)))
            {
                return (IRepository<TEntity>)_repositories[typeof(TEntity)];
            }

            var repository = new Repository<TEntity>(_context, _httpContextAccessor);
            _repositories[typeof(TEntity)] = repository;

            return repository;
        }

        public IDbContextTransaction BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
            return _transaction;
        }

        public void CommitTransaction()
        {
            _transaction?.Commit();
            _transaction = null;
        }

        public void RollbackTransaction()
        {
            _transaction?.Rollback();
            _transaction = null;
        }

        public void SaveChanges()
        {
            // Save changes
            _context.SaveChanges();
        }
    }
}