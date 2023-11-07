namespace FlowSheetAPI.Repository.Interfaces
{
    public interface ILookupRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(Guid id);
    }
}
