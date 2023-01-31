using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
namespace Farmasi.Core.Helper
{
    public class BaseActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var cartId = context.HttpContext.Session.GetString("CartId");
            if (!string.IsNullOrEmpty(cartId))
                context.HttpContext.Session.SetString("CartId", Guid.NewGuid().ToString());
        }
    }
}