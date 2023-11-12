using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace RegLog.Models
{
    public class SessionCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string? email = context.HttpContext.Session.GetString("Email");

            if (email == null)
            {
                context.Result = new RedirectToActionResult("Index", "Home", null);
            }
        }
    }
}
