using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Farmasi.Core.Domain.Categories;
using Farmasi.Service.Categories.Input;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Farmasi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {

        [HttpPost("AddProductToBasket")]
        [SwaggerOperation(Summary = "Basket", Description = "AddProductToBasket")]
        public IActionResult Add(Guid Uid)
        {           
            return Ok();
        }
        [HttpDelete("RemoveItemFromBasket")]
        [SwaggerOperation(Summary = "Basket", Description = "RemoveItemFromBasket")]
        public IActionResult Remove(Guid Uid)
        {
            return Ok();
        }
        [HttpPost("GetBaset")]
        [SwaggerOperation(Summary = "Basket", Description = "Get Baset")]
        public IActionResult List()
        {
            return Ok();
        }
    }
}

