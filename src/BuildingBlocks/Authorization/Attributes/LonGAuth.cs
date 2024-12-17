using Authorization.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
namespace Authorization.Attributes;

public class LonGAuth : Attribute, IAsyncAuthorizationFilter
{
    private string _scope = string.Empty; 
    private string _operation = string.Empty; 

    public LonGAuth()
    {

    }


    public LonGAuth(string scope, string operation)
    {
        _scope = scope;
        _operation = operation;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (context.HttpContext.User.Identity.IsAuthenticated)
        {
            var myCustomAuthService = context.HttpContext.RequestServices.GetRequiredService<ICustomAuthService>();

            if (!await myCustomAuthService.CheckIfAllowed(_scope, _operation))
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
