namespace FlowSheetAPI.Services.Interfaces
{
    public interface ICacheService
    {
        public Task SetDataAsync<T>(string key, List<T> values, TimeSpan expiration);
        public Task<List<T>?> GetDataAsync<T>(string key);
        public void InvalidateAsync(string key);
    }
}
