using Authorization.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
namespace Authorization.Attributes;

public class LonGAuth : Attribute, IAsyncAuthorizationFilter
{
    private readonly string condition = string.Empty; 

    public LonGAuth()
    {

    }


    public LonGAuth(string condition)
    {
        this.condition = condition;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (context.HttpContext.User.Identity.IsAuthenticated)
        {
            var myCustomAuthService = context.HttpContext.RequestServices.GetRequiredService<ICustomAuthService>();

            if (!await myCustomAuthService.CheckIfAllowed(condition))
            {
                context.Result = new ForbidResult();
            }
        }
        else
        {
            context.Result = new UnauthorizedResult();
        }
    }
}
