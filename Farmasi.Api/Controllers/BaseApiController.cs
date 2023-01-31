using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Farmasi.Core.Helper;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Farmasi.Api.Controllers
{ 
    [ServiceFilter(typeof(BaseActionFilter))]
    public class BaseApiController : ControllerBase
    {       
    }
}

