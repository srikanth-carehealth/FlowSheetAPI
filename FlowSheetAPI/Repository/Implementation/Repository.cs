using System.Linq.Expressions;
using FlowSheetAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FlowSheetAPI.Repository.Implementation
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly FlowSheetDbContext Context;

        public Repository(FlowSheetDbContext context)
        {
            Context = context;
        }

        public TEntity? GetById(Guid id) 
            => Context.Set<TEntity>().Find(id);

        public IQueryable<TEntity> GetAll() 
            => Context.Set<TEntity>().Include(i => i);

        public async Task<TEntity?> GetByIdAsync(Guid id) =>
            await Context.Set<TEntity>().Include(i => i).FirstOrDefaultAsync();

        public async Task<IEnumerable<TEntity>> GetAllAsync()
            => await Context.Set<TEntity>().ToListAsync();

        public async Task AddAsync(TEntity entity)
            => await Context.Set<TEntity>().AddAsync(entity);

        public void Update(TEntity entity)
            => Context.Set<TEntity>().Update(entity);

        public void Delete(TEntity entity)
            => Context.Set<TEntity>().Remove(entity);

        public async Task SaveChangesAsync()
            => await Context.SaveChangesAsync();

        public async Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate)
            => await Context.Set<TEntity>().Include(i => i).Where(predicate).ToListAsync();

        public TEntity Get(Expression<Func<TEntity?, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity?> query = Context.Set<TEntity>();

            foreach (var includeProperty in includeProperties)
            {
                query = query?.Include(includeProperty);
            }

            return query.SingleOrDefault(predicate);
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return predicate != null ? query.Where(predicate).ToList() : query.ToList();
        }
    }
}
