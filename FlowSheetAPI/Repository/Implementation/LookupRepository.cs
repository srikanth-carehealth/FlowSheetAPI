using FlowSheetAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FlowSheetAPI.Repository.Implementation
{
    public class LookupRepository<TEntity> : ILookupRepository<TEntity> where TEntity : class
    {
        protected readonly FlowSheetDbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public LookupRepository(FlowSheetDbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
            => await DbSet.ToListAsync();


        public async Task<TEntity?> GetByIdAsync(Guid id)
            => await DbSet.FindAsync(id);
    }
}
