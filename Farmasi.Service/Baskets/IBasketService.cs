using System;
using Farmasi.Service.Products.Dto;
using Nancy.Session;

namespace Farmasi.Service.Baskets
{
    public interface IBasketService
    {
        ProductDto AddBasket(string sessionId, ProductDto product);
        ProductDto RemoveBasket(string sessionId, ProductDto product);
        List<ProductDto> ListBasket(string sessionId);
    }
}

