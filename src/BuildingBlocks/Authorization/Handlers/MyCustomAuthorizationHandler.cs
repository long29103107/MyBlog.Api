using Authorization.Attributes;
using Authorization.Services;
using Microsoft.AspNetCore.Authorization;

namespace Authorization.Handlers;

public class MyCustomAuthorizationHandler(ICustomAuthService customAuthService) : AuthorizationHandler<ClaimRequirementAttribute>
{

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimRequirementAttribute requirement)
    {
        if (await customAuthService.CheckIfAllowedAsync(requirement.UserId, requirement.Scope, requirement.Operation))
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
    }

}