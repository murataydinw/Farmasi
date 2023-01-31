using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Farmasi.Core.Domain.Produtcs;
using Farmasi.Service.Categories;
using Farmasi.Service.Categories.Dto;
using Farmasi.Service.Categories.Input;
using Farmasi.Service.Products;
using Farmasi.Service.Products.Dto;
using Farmasi.Service.Products.Input;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Farmasi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseApiController
    {
        private readonly ICategoryService _category;
        private readonly IProductService _product;
        public CategoryController(ICategoryService category, IProductService product)
        {
            _category = category;
            _product = product;
        }


        [HttpPost("GetCategory")]
        [SwaggerOperation(Summary = "Category", Description = "Category Detail With Name or Url")]
        public IActionResult Detail(CategoryDetailParameter input)
        {
            var model = _category.GetDetail(input);
            return Ok(model);
        }
        [HttpGet("GetCategoryList")]
        [SwaggerOperation(Summary = "Category", Description = "Category List With CategoryName or ProductName Contains")]
        public IActionResult List()
        {
            var parameter = new CategoryListParameter { Skip = 0, Take = 10 };
            var model = _category.GetList(parameter);
            return Ok(model);
        }       
        [HttpPost("SaveCategory")]
        [SwaggerOperation(Summary = "Category", Description = "Category Save")]
        public IActionResult Save(CategoryDto input)
        {
            var model = _category.Save(input);
            return Ok(model);
        }
    }
}

