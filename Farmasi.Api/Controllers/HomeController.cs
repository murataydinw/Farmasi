using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Farmasi.Service.Categories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Farmasi.Api.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _category;
        public HomeController(ICategoryService category)
        {
            _category = category;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            //var save = _category.Save("Viskyuv", "ekrem");
            var detail = _category.GetDetail(url: "ekrem");
            return Ok(detail);
        }
    }
}

