using System.IO.Compression;
using System.Text;
using System.Text.Json;
using FlowSheetAPI.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace FlowSheetAPI.Services.Implementation
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async void InvalidateAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }

        public async Task SetDataAsync<T>(string key, List<T> values, TimeSpan expiration)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration
            };

            var bytes = Serialize(values);
            var compressedBytes = Compress(bytes);
            await _cache.SetAsync(key, compressedBytes, options);
        }

        public async Task<List<T>?> GetDataAsync<T>(string key)
        {
            var compressedBytes = await _cache.GetAsync(key);

            if (compressedBytes == null) return default;

            var bytes = Decompress(compressedBytes);
            var values = Deserialize<T>(bytes);

            return values;
        }

        private byte[] Serialize<T>(List<T> values)
        {
            var jsonString = JsonSerializer.Serialize(values, _jsonOptions);
            return Encoding.UTF8.GetBytes(jsonString);
        }

        private List<T>? Deserialize<T>(byte[] bytes)
        {
            var jsonString = Encoding.UTF8.GetString(bytes);
            return JsonSerializer.Deserialize<List<T>>(jsonString, _jsonOptions);
        }

        private static byte[] Compress(byte[] bytes)
        {
            using var stream = new MemoryStream();
            using var gzip = new GZipStream(stream, CompressionMode.Compress);
            gzip.Write(bytes, 0, bytes.Length);
            gzip.Close();
            return stream.ToArray();
        }

        private static byte[] Decompress(byte[] bytes)
        {
            using var stream = new MemoryStream(bytes);
            using var gzip = new GZipStream(stream, CompressionMode.Decompress);
            using var output = new MemoryStream();
            gzip.CopyTo(output);
            return output.ToArray();
        }

        private readonly JsonSerializerOptions? _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }
}
