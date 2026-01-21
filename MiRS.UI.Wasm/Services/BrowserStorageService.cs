using Microsoft.JSInterop;
using System.Text.Json;

namespace MiRS.UI.Wasm.Services
{
    public class BrowserStorageService
    {
        private readonly IJSRuntime _js;

        private TimeSpan _defaultExpiration = TimeSpan.FromMinutes(5);

        public BrowserStorageService(IJSRuntime js)
        {
            _js = js;
        }

        public void SetDefaultExpiration(TimeSpan expiration)
        {
            _defaultExpiration = expiration;
        }

        public async ValueTask SetToLocalStorage<T>(string key, T value, TimeSpan? expiration = null)
        {
            var cache = new TimedCache<T>
            {
                Value = value,
                ExpiresAt = DateTime.UtcNow.Add(expiration ?? _defaultExpiration)
            };

            var json = JsonSerializer.Serialize(cache);
            await _js.InvokeVoidAsync("localStorage.setItem", key, json);

        }


        public async ValueTask<T?> GetFromLocalStorage<T>(string key, string? defaultValue = null)
        {

            var json = await _js.InvokeAsync<string>("localStorage.getItem", key);

            if (string.IsNullOrEmpty(json)) return default;

            try
            {
                var cache = JsonSerializer.Deserialize<TimedCache<T>>(json);
                if (cache == null || cache.ExpiresAt <= DateTime.UtcNow)
                {
                    // Expired — remove it
                    await RemoveFromLocalStorage(key);
                    return default;
                }
                return cache.Value;
            }
            catch
            {
                await RemoveFromLocalStorage(key);
                return default;
            }
        }

        public async ValueTask RemoveFromLocalStorage(string key) => await _js.InvokeVoidAsync("localStorage.removeItem", key);

        public ValueTask ClearAsync() => _js.InvokeVoidAsync("localStorage.clear");

        public async ValueTask SetToSessionStorage<T>(string key, T value, TimeSpan? expiration = null)
        {
            var cache = new TimedCache<T>
            {
                Value = value,
                ExpiresAt = DateTime.UtcNow.Add(expiration ?? _defaultExpiration)
            };

            var json = JsonSerializer.Serialize(cache);
            await _js.InvokeVoidAsync("sessionStorage.setItem", key, json);
        }


        public async ValueTask<T?> GetFromSessionStorage<T>(string key, string? defaultValue = null)
        {
            var json = await _js.InvokeAsync<string>("sessionStorage.getItem", key);

            if (string.IsNullOrEmpty(json)) return default;

            try
            {
                var cache = JsonSerializer.Deserialize<TimedCache<T>>(json);
                if (cache == null || cache.ExpiresAt <= DateTime.UtcNow)
                {
                    // Expired — remove it
                    await RemoveFromLocalStorage(key);
                    return default;
                }
                return cache.Value;
            }
            catch
            {
                await RemoveFromLocalStorage(key);
                return default;
            }
        }


        public async ValueTask RemoveFromSessionStorage(string key) => await _js.InvokeVoidAsync("sessionStorage.removeItem", key);

        public async ValueTask ClearFromSessionStorage(string key) => await _js.InvokeVoidAsync("sessionStorage.clear", key);

        /// <summary>
        /// Wrapper class to store value + expiration
        /// </summary>
        private class TimedCache<T>
        {
            public T? Value { get; set; }
            public DateTime ExpiresAt { get; set; }
        }
    }

}
