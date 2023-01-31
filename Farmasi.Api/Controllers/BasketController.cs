using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Farmasi.Core.Domain.Categories;
using Farmasi.Core.Domain.Produtcs;
using Farmasi.Service.Baskets;
using Farmasi.Service.Categories.Input;
using Farmasi.Service.Products.Dto;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Farmasi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : BaseApiController
    {
        private readonly IBasketService _basketService;
        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }
        [HttpPost("AddProductToBasket")]
        [SwaggerOperation(Summary = "Basket", Description = "AddProductToBasket")]
        public IActionResult Add(ProductDto product)
        {
            var cartId = HttpContext.Session.GetString("cartId");
            var result = _basketService.AddBasket(cartId, product);
            return Ok(result);
        }
        [HttpDelete("RemoveItemFromBasket")]
        [SwaggerOperation(Summary = "Basket", Description = "RemoveItemFromBasket")]
        public IActionResult Remove(Guid productId)
        {
            var cartId = HttpContext.Session.GetString("cartId");
            var result = _basketService.RemoveBasket(cartId, productId);
            return Ok(result);
        }
        [HttpPost("GetBaset")]
        [SwaggerOperation(Summary = "Basket", Description = "Get Baset")]
        public IActionResult List()
        {
            var cartId = HttpContext.Session.GetString("cartId");
            var result = _basketService.ListBasket(cartId);
            return Ok(result);
        }
    }
}

