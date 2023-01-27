using Farmasi.Core.Caching;

namespace Farmasi.Core.Caching
{
    public class CacheKey
    {
        #region Ctor
        public CacheKey(string key, int cacheTime)
        {
            Key = key;
            CacheTime = cacheTime;
            CacheTimePeriod = CacheTimePeriod.Minute;
        }
        public CacheKey(string key, int cacheTime, int cacheSlidingTime)
        {
            Key = key;
            CacheTime = cacheTime;
            CacheSlidingTime = cacheSlidingTime;
            CacheTimePeriod = CacheTimePeriod.Minute;
        }
        public CacheKey(string key, int cacheTime, CacheTimePeriod cacheTimePeriod)
        {
            Key = key;
            CacheTime = cacheTime;
            CacheTimePeriod = cacheTimePeriod;
        }
        public CacheKey(string key, int cacheTime, int cacheSlidingTime, CacheTimePeriod cacheTimePeriod)
        {
            Key = key;
            CacheTime = cacheTime;
            CacheSlidingTime = cacheSlidingTime;
            CacheTimePeriod = cacheTimePeriod;
        }
        #endregion
        public string Key { get; protected set; }
        public int CacheTime { get; set; }
        public int? CacheSlidingTime { get; set; }
        public CacheTimePeriod CacheTimePeriod { get; set; }
    }
}
