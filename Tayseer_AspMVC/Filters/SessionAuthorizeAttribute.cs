using Microsoft.AspNetCore.Mvc.Filters;

namespace Tayseer_AspMVC.Filters
{
    public class SessionAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var username = context.HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                context.HttpContext.Response.Redirect("/Account/Login");
            }
            base.OnActionExecuting(context);
        }
    }
}
