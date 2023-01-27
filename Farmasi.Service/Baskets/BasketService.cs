using System;
using Farmasi.Core.Caching;
using Farmasi.Core.Domain.Produtcs;
using Farmasi.Service.Products.Dto;

namespace Farmasi.Service.Baskets
{
    public class BasketService : IBasketService
    {
        private readonly ICacheManager _cacheManager;
        public BasketService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }
     

        public ProductDto AddBasket(string sessionId, ProductDto product)
        {
            string key = $"{sessionId}";
            var cacheKey = new CacheKey(key, cacheTime: 1, 1, CacheTimePeriod.Month);
            var result = _cacheManager.GetOrCreate(cacheKey, () =>
            {
                
                return product;
            });
            return result;
        }
        public ProductDto RemoveBasket(string sessionId, ProductDto product)
        {
            string key = $"{sessionId}";
            var cacheKey = new CacheKey(key, cacheTime: 1, 1, CacheTimePeriod.Month);
            var result = _cacheManager.GetOrCreate(cacheKey, () =>
            {
               
                return product;
            });
            return result;
        }

        public List<ProductDto> ListBasket(string sessionId)
        {
            string key = $"{sessionId}";
            var cacheKey = new CacheKey(key, cacheTime: 1, 1, CacheTimePeriod.Month);
            var result = _cacheManager.GetOrCreate<List<ProductDto>>(cacheKey, () =>
            {
                
                return new List<ProductDto>();
            });
            return result;
        }
    }
}

