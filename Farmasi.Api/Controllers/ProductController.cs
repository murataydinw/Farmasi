using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Farmasi.Service.Categories;
using Farmasi.Service.Categories.Input;
using Farmasi.Service.Products;
using Farmasi.Service.Products.Dto;
using Farmasi.Service.Products.Input;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Farmasi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseApiController
    {
        private readonly ICategoryService _category;
        private readonly IProductService _product;
        public ProductController(ICategoryService category, IProductService product)
        {
            _category = category;
            _product = product;
        }
       
        [HttpPost("GetProduct")]
        [SwaggerOperation(Summary = "Product", Description = "Product Detail With Name or Url")]
        public IActionResult Detail(ProductDetailParameter input)
        {          
            var model = _product.GetDetail(input);
            return Ok(model);
        }
        [HttpGet("ProductListAll")]
        [SwaggerOperation(Summary = "Product", Description = "Product List With CategoryName or ProductName Contains")]
        public IActionResult List()
        {
            var parameter = new ProductListParameter { Skip = 0, Take = 100 };
            var model = _product.GetList(parameter);
            return Ok(model);
        }
        [HttpGet("ProductList")]
        [SwaggerOperation(Summary = "Product", Description = "Product List With CategoryName or ProductName Contains")]
        public IActionResult List(ProductListParameter input)
        {
            var model = _product.GetList(input);
            return Ok(model);
        }
        [HttpPost("SaveProduct")]
        [SwaggerOperation(Summary = "Product", Description = "Praduct Save")]
        public IActionResult Save(ProductDto input)
        {
            var model = _product.Save(input);
            return Ok(model);
        }
    }
}


