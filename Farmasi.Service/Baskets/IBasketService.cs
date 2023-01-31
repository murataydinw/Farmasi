using System;
using Farmasi.Service.Products.Dto;
using Nancy.Session;

namespace Farmasi.Service.Baskets
{
    public interface IBasketService
    {
        List<ProductDto> AddBasket(string sessionId, ProductDto product);
        List<ProductDto> RemoveBasket(string sessionId, Guid productId);
        List<ProductDto> ListBasket(string sessionId);
    }
}

