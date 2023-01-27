using Farmasi.Core.Helper;
using Microsoft.Extensions.Caching.Distributed;

namespace Farmasi.Core.Caching
{
    public class RedisDistributedCacheManager : ICacheManager
    {
        private readonly IDistributedCache _distributedCache;

        public RedisDistributedCacheManager(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public T Get<T>(string key)
        {
            var byteArray = _distributedCache.Get(key);
            var model = ConverterHelper.ByteArrayToModel<T>(byteArray);
            return model;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var byteArray = await _distributedCache.GetAsync(key);
            var model = ConverterHelper.ByteArrayToModel<T>(byteArray);
            return model;
        }

        public T GetOrCreate<T>(CacheKey cacheKey, Func<T> acquire)
        {
            if (cacheKey.CacheTime <= 0 && cacheKey.CacheSlidingTime is null or <= 0)
            {
                return acquire();
            }

            var cacheByteArray = _distributedCache.Get(cacheKey.Key);
            if (cacheByteArray == null)
            {
                var model = acquire();
                if (model != null)
                {
                    Set(cacheKey, model);
                }
                return model;
            }

            var cacheModel = ConverterHelper.ByteArrayToModel<T>(cacheByteArray);
            return cacheModel;
        }

        public async Task<T> GetOrCreateAsync<T>(CacheKey cacheKey, Func<T> acquire)
        {
            if (cacheKey.CacheTime <= 0 && (cacheKey.CacheSlidingTime == null || cacheKey.CacheSlidingTime <= 0))
            {
                return acquire();
            }

            var cacheByteArray = await _distributedCache.GetAsync(cacheKey.Key);
            if (cacheByteArray == null)
            {
                var model = acquire();
                if (model != null)
                {
                    await SetAsync(cacheKey, model);
                }
                return model;
            }

            var cacheModel = ConverterHelper.ByteArrayToModel<T>(cacheByteArray);
            return cacheModel;
        }

        public async Task<T> GetOrCreateAsync<T>(CacheKey cacheKey, Func<Task<T>> acquire)
        {
            if (cacheKey.CacheTime <= 0 && (cacheKey.CacheSlidingTime == null || cacheKey.CacheSlidingTime <= 0))
            {
                return await acquire();
            }

            var cacheByteArray = await _distributedCache.GetAsync(cacheKey.Key);
            if (cacheByteArray == null)
            {
                var model = await acquire();
                if (model != null)
                {
                    await SetAsync(cacheKey, model);
                }
                return model;
            }

            var cacheModel = ConverterHelper.ByteArrayToModel<T>(cacheByteArray);
            return cacheModel;
        }

        public byte[] Get(string key)
        {
            var byteArray = _distributedCache.Get(key);
            return byteArray;
        }

        public async Task<byte[]> GetAsync(string key)
        {
            var byteArray = await _distributedCache.GetAsync(key);
            return byteArray;
        }

        public byte[] GetOrCreate(CacheKey cacheKey, Func<byte[]> acquire)
        {
            if (cacheKey.CacheTime <= 0 && (cacheKey.CacheSlidingTime == null || cacheKey.CacheSlidingTime <= 0))
            {
                return acquire();
            }

            var cacheByteArray = _distributedCache.Get(cacheKey.Key);
            if (cacheByteArray == null)
            {
                var byteArray = acquire();
                if (byteArray != null)
                {
                    Set(cacheKey, byteArray);
                }
                return byteArray;
            }
            return cacheByteArray;
        }

        public async Task<byte[]> GetOrCreateAsync(CacheKey cacheKey, Func<byte[]> acquire)
        {
            if (cacheKey.CacheTime <= 0 && (cacheKey.CacheSlidingTime == null || cacheKey.CacheSlidingTime <= 0))
            {
                return acquire();
            }

            var cacheByteArray = await _distributedCache.GetAsync(cacheKey.Key);
            if (cacheByteArray == null)
            {
                var byteArray = acquire();
                if (byteArray != null)
                {
                    await SetAsync(cacheKey, byteArray);
                }
                return byteArray;
            }
            return cacheByteArray;
        }

        public string GetString(string key)
        {
            var value = _distributedCache.GetString(key);
            return value;
        }

        public async Task<string> GetStringAsync(string key)
        {
            var value = await _distributedCache.GetStringAsync(key);
            return value;
        }

        public string GetOrCreateString(CacheKey cacheKey, Func<string> acquire)
        {
            if (cacheKey.CacheTime <= 0 && (cacheKey.CacheSlidingTime == null || cacheKey.CacheSlidingTime <= 0))
            {
                return acquire();
            }

            var cacheValue = _distributedCache.GetString(cacheKey.Key);
            if (string.IsNullOrEmpty(cacheValue))
            {
                var value = acquire();
                if (string.IsNullOrEmpty(value))
                {
                    SetString(cacheKey, value);
                }
                return value;
            }
            return cacheValue;
        }

        public async Task<string> GetOrCreateStringAsync(CacheKey cacheKey, Func<string> acquire)
        {
            if (cacheKey.CacheTime <= 0 && (cacheKey.CacheSlidingTime == null || cacheKey.CacheSlidingTime <= 0))
            {
                return acquire();
            }

            var cacheValue = await _distributedCache.GetStringAsync(cacheKey.Key);
            if (string.IsNullOrEmpty(cacheValue))
            {
                var value = acquire();
                if (string.IsNullOrEmpty(value))
                {
                    await SetStringAsync(cacheKey, value);
                }
                return value;
            }
            return cacheValue;
        }

        public void Set(CacheKey cacheKey, object model)
        {
            var distributedCacheEntryOptions = PrepareDistributedCacheEntryOptions(cacheKey);
            var byteArray = ConverterHelper.ModelToByteArray(model);
            _distributedCache.Set(cacheKey.Key, byteArray, distributedCacheEntryOptions);
        }

        public Task SetAsync(CacheKey cacheKey, object model)
        {
            var memoryCacheEntryOptions = PrepareDistributedCacheEntryOptions(cacheKey);
            var byteArray = ConverterHelper.ModelToByteArray(model);
            _distributedCache.SetAsync(cacheKey.Key, byteArray, memoryCacheEntryOptions);
            return Task.CompletedTask;
        }

        public void Set(CacheKey cacheKey, byte[] byteArray)
        {
            var distributedCacheEntryOptions = PrepareDistributedCacheEntryOptions(cacheKey);
            _distributedCache.Set(cacheKey.Key, byteArray, distributedCacheEntryOptions);
        }

        public Task SetAsync(CacheKey cacheKey, byte[] byteArray)
        {
            var memoryCacheEntryOptions = PrepareDistributedCacheEntryOptions(cacheKey);
            _distributedCache.SetAsync(cacheKey.Key, byteArray, memoryCacheEntryOptions);
            return Task.CompletedTask;
        }

        public void SetString(CacheKey cacheKey, string value)
        {
            var memoryCacheEntryOptions = PrepareDistributedCacheEntryOptions(cacheKey);
            _distributedCache.SetString(cacheKey.Key, value, memoryCacheEntryOptions);
        }

        public Task SetStringAsync(CacheKey cacheKey, string value)
        {
            var memoryCacheEntryOptions = PrepareDistributedCacheEntryOptions(cacheKey);
            _distributedCache.SetStringAsync(cacheKey.Key, value, memoryCacheEntryOptions);
            return Task.CompletedTask;
        }

        public void Remove(string key)
        {
            _distributedCache.Remove(key);
        }

        public Task RemoveAsync(string key)
        {
            _distributedCache.Remove(key);
            return Task.CompletedTask;
        }

        private static DistributedCacheEntryOptions PrepareDistributedCacheEntryOptions(CacheKey cacheKey)
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

            var distributedCacheEntryOptions = new DistributedCacheEntryOptions
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
                distributedCacheEntryOptions.SlidingExpiration = slidingExpiration;
            }
            return distributedCacheEntryOptions;
        }
    }
}
