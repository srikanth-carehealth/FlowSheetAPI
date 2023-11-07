using System.Security.Claims;
using FlowSheetAPI.DomainModel;
using FlowSheetAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
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

            var repository = new Repository<TEntity>(_context);
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
            // Get all added or modified entities
            var addedOrModifiedEntities = _context.ChangeTracker.Entries()
                .Where(x => x is { Entity: BaseEntity, State: EntityState.Added or EntityState.Modified });

            var loggedInUser = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Set the properties for added or modified entities
            foreach (var entry in addedOrModifiedEntities)
            {
                var entity = (BaseEntity)entry.Entity;
                var now = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    entity.Id = Guid.NewGuid();
                    // Get the current logged in user id
                    entity.CreatedBy = loggedInUser;
                    entity.CreatedDate = now;
                }
                // Get the current logged in user id
                entity.UpdatedBy = loggedInUser;
                entity.UpdatedDate = now;
            }

            // Save changes
            _context.SaveChanges();
        }
    }
}
