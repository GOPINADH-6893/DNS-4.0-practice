using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplication1.Filters
{
    public class CustAuthFilter : ActionFilterAttribute
    {
       public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Custom authentication logic can be implemented here
            // For example, checking for a specific header or token
            if (!context.HttpContext.Request.Headers.TryGetValue("Authorization",out var token))
            {
                context.Result = new BadRequestObjectResult("Invalid request - No Auth token");
                return;
            }
            if (!token.ToString().Contains("Bearer", StringComparison.OrdinalIgnoreCase))
            {
                context.Result = new BadRequestObjectResult("Invalid request - Token present but Bearer unavailable");
                return;
            }
            base.OnActionExecuting(context);
        }
    }
}
