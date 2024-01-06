using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using FlowSheetAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FlowSheetAPI.Repository.Implementation
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly FlowSheetDbContext Context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Repository(FlowSheetDbContext context, 
                            IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public TEntity? GetById(Guid id)
            => Context.Set<TEntity>().Find(id);

        public IQueryable<TEntity> GetAll()
            => Context.Set<TEntity>().Include(i => i);

        public async Task<TEntity?> GetByIdAsync(Guid id) =>
            await Context.Set<TEntity>().FindAsync(id); //.Include(i => i)

        public async Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return await Task.FromResult(query.ToList());
        }

        public async Task UpsertAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var rowVersionPropertyInfo = GetPropertyInfo(entity, "RowVersion");
            var rowVersionObject = rowVersionPropertyInfo?.GetValue(entity);
            var entityType = entity.GetType();

            // Get the current logged in user id
            var loggedInUser = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                               "System";

            // If RowVersion is not null and not empty, we assume the entity exists
            if (rowVersionObject is byte[] { Length: > 0 })
            {
                // If the entity exists, attach it to the context and mark it as modified
                Context.Set<TEntity>().Attach(entity);
                Context.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                // Set the RowVersion property
                entity.GetType().GetProperty("RowVersion")?.SetValue(entity, Encoding.UTF8.GetBytes(DateTime.Now.ToString(CultureInfo.InvariantCulture)));
                
                // Set the primary key property
                var entityKeyProperty = GetPrimaryKeyProperty(entity);
                if (entityKeyProperty.PropertyType == typeof(Guid))
                {
                    entityKeyProperty.SetValue(entity, Guid.NewGuid());
                }

                // Get the current logged in user id
                if (entityType.GetProperty("CreatedBy") is { } createdByProp) createdByProp.SetValue(entity, loggedInUser);
                if (entityType.GetProperty("CreatedDate") is { } createdDateProp) createdDateProp.SetValue(entity, DateTime.UtcNow);

                // If the entity does not exist, add it to the context
                Context.Set<TEntity>().Add(entity);
                Context.Entry(entity).State = EntityState.Added;
            }

            // Get the current logged in user id
            if (entityType.GetProperty("UpdatedBy") is { } updatedByProp) updatedByProp.SetValue(entity, loggedInUser);
            if (entityType.GetProperty("UpdatedDate") is { } updatedDateProp) updatedDateProp.SetValue(entity, DateTime.UtcNow);
        }

        public void Delete(TEntity entity)
            => Context.Set<TEntity>().Remove(entity);

        public async Task SaveChangesAsync()
            => await Context.SaveChangesAsync();

        public async Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate)
            => await Context.Set<TEntity>().Where(predicate).ToListAsync(); //.Include(i => i)

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

        // Give the property when an entity and property name is given as input parameter
        private PropertyInfo? GetPropertyInfo(TEntity entity, string propertyName)
        {
            var type = entity.GetType();
            return type.GetProperty(propertyName);
        }

        private static PropertyInfo GetPrimaryKeyProperty(object entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var keyProperty = entity.GetType().GetProperties()
                .FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(KeyAttribute)));

            if (keyProperty == null)
            {
                throw new InvalidOperationException("Primary key attribute not found.");
            }

            return keyProperty;
        }

        public static byte[] SerializeObject(object obj)
        {
            var jsonString = JsonConvert.SerializeObject(obj);
            return System.Text.Encoding.UTF8.GetBytes(jsonString);
        }
    }
}
