using Microsoft.Extensions.Caching.Memory;

namespace Farmasi.Core.Caching
{
    public class InMemoryCacheManager : ICacheManager
    {
        private readonly IMemoryCache _memoryCache;
        public InMemoryCacheManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public T Get<T>(string key)
        {
            _memoryCache.TryGetValue(key, out T model);
            return model;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            return await Task.Run(() =>
            {
                _memoryCache.TryGetValue(key, out T model);
                return model;
            });
        }

        public T GetOrCreate<T>(CacheKey cacheKey, Func<T> acquire)
        {
            if (cacheKey.CacheTime <= 0 && cacheKey.CacheSlidingTime is null or <= 0)
            {
                return acquire();
            }

            var result = _memoryCache.GetOrCreate(cacheKey.Key, entry =>
            {
                entry.SetOptions(PrepareMemoryCacheEntryOptions(cacheKey));

                return acquire();
            });

            if (result == null)
            {
                Remove(cacheKey.Key);
            }

            return result;
        }

        public async Task<T> GetOrCreateAsync<T>(CacheKey cacheKey, Func<T> acquire)
        {
            if (cacheKey.CacheTime <= 0 && (cacheKey.CacheSlidingTime == null || cacheKey.CacheSlidingTime <= 0))
            {
                return acquire();
            }

            var result = _memoryCache.GetOrCreate(cacheKey.Key, entry =>
            {
                entry.SetOptions(PrepareMemoryCacheEntryOptions(cacheKey));

                return acquire();
            });

            if (result == null)
            {
                await RemoveAsync(cacheKey.Key);
            }

            return result;
        }

        public async Task<T> GetOrCreateAsync<T>(CacheKey cacheKey, Func<Task<T>> acquire)
        {
            if (cacheKey.CacheTime <= 0 && cacheKey.CacheSlidingTime is null or <= 0)
            {
                return await acquire();
            }

            var result = await _memoryCache.GetOrCreateAsync(cacheKey.Key, async entry =>
            {
                entry.SetOptions(PrepareMemoryCacheEntryOptions(cacheKey));

                return await acquire();
            });

            if (result == null)
            {
                await RemoveAsync(cacheKey.Key);
            }

            return result;
        }

        public byte[] Get(string key)
        {
            return Get<byte[]>(key);
        }

        public Task<byte[]> GetAsync(string key)
        {
            return GetAsync<byte[]>(key);
        }

        public byte[] GetOrCreate(CacheKey cacheKey, Func<byte[]> acquire)
        {
            return GetOrCreate<byte[]>(cacheKey, acquire);
        }

        public Task<byte[]> GetOrCreateAsync(CacheKey cacheKey, Func<byte[]> acquire)
        {
            return GetOrCreateAsync<byte[]>(cacheKey, acquire);
        }

        public string GetString(string key)
        {
            return Get<string>(key);
        }

        public Task<string> GetStringAsync(string key)
        {
            return GetAsync<string>(key);
        }

        public string GetOrCreateString(CacheKey cacheKey, Func<string> acquire)
        {
            return GetOrCreate<string>(cacheKey, acquire);
        }

        public Task<string> GetOrCreateStringAsync(CacheKey cacheKey, Func<string> acquire)
        {
            return GetOrCreateAsync<string>(cacheKey, acquire);
        }

        public void Set(CacheKey cacheKey, object model)
        {
            var memoryCacheEntryOptions = PrepareMemoryCacheEntryOptions(cacheKey);
            _memoryCache.Set(cacheKey.Key, model, memoryCacheEntryOptions);
        }

        public Task SetAsync(CacheKey cacheKey, object model)
        {
            return SetAsyncForInMemory(cacheKey, model);
        }

        public void Set(CacheKey cacheKey, byte[] byteArray)
        {
            SetForInMemory(cacheKey, byteArray);
        }

        public Task SetAsync(CacheKey cacheKey, byte[] byteArray)
        {
            return SetAsyncForInMemory(cacheKey, byteArray);
        }

        public void SetString(CacheKey cacheKey, string value)
        {
            SetForInMemory(cacheKey, value);
        }

        public Task SetStringAsync(CacheKey cacheKey, string value)
        {
            return SetAsyncForInMemory(cacheKey, value);
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public Task RemoveAsync(string key)
        {
            _memoryCache.Remove(key);
            return Task.CompletedTask;
        }

        private void SetForInMemory(CacheKey cacheKey, object model)
        {
            var memoryCacheEntryOptions = PrepareMemoryCacheEntryOptions(cacheKey);
            _memoryCache.Set(cacheKey.Key, model, memoryCacheEntryOptions);
        }

        private Task SetAsyncForInMemory(CacheKey cacheKey, object model)
        {
            var memoryCacheEntryOptions = PrepareMemoryCacheEntryOptions(cacheKey);
            _memoryCache.Set(cacheKey.Key, model, memoryCacheEntryOptions);
            return Task.CompletedTask;
        }

        private static MemoryCacheEntryOptions PrepareMemoryCacheEntryOptions(CacheKey cacheKey)
        {
            var absoluteExpirationRelativeToNow = cacheKey.CacheTimePeriod switch
            {
                CacheTimePeriod.Second => TimeSpan.FromSeconds(cacheKey.CacheTime),
                CacheTimePeriod.Minute => TimeSpan.FromMinutes(cacheKey.CacheTime),
                CacheTimePeriod.Hour => TimeSpan.FromHours(cacheKey.CacheTime),
                CacheTimePeriod.Day => TimeSpan.FromDays(cacheKey.CacheTime),
                CacheTimePeriod.Week => TimeSpan.FromDays(cacheKey.CacheTime * 7),
                CacheTimePeriod.Month => DateTime.Now.AddMonths(cacheKey.CacheTime) - DateTime.Now,
                CacheTimePeriod.Year => DateTime.Now.AddYears(cacheKey.CacheTime) - DateTime.Now,
                _ => TimeSpan.FromMinutes(cacheKey.CacheTime)
            };
            var memoryCacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow
            };

            if (cacheKey.CacheSlidingTime > 0)
            {
                var slidingTime = (int)cacheKey.CacheSlidingTime;
                var slidingExpiration = cacheKey.CacheTimePeriod switch
                {
                    CacheTimePeriod.Second => TimeSpan.FromSeconds(slidingTime),
                    CacheTimePeriod.Minute => TimeSpan.FromMinutes(slidingTime),
                    CacheTimePeriod.Hour => TimeSpan.FromHours(slidingTime),
                    CacheTimePeriod.Day => TimeSpan.FromDays(slidingTime),
                    CacheTimePeriod.Week => TimeSpan.FromDays(slidingTime * 7),
                    CacheTimePeriod.Month => DateTime.Now.AddMonths(slidingTime) - DateTime.Now,
                    CacheTimePeriod.Year => DateTime.Now.AddYears(slidingTime) - DateTime.Now,
                    _ => TimeSpan.FromMinutes(cacheKey.CacheTime)
                };
                memoryCacheEntryOptions.SlidingExpiration = slidingExpiration;
            }
            //if (cacheKey.CacheItemPriority != null)
            //{
            //    memoryCacheEntryOptions.Priority = (CacheItemPriority)cacheKey.CacheItemPriority;
            //}

            return memoryCacheEntryOptions;
        }
    }
}
