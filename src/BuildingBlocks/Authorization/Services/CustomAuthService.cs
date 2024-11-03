using Microsoft.AspNetCore.Http;

namespace Authorization.Services;

public class CustomAuthService(IHttpContextAccessor contextAccessor) : ICustomAuthService
{
    public Task<bool> CheckIfAllowed(string condition)
    {
        return Task.FromResult(string.IsNullOrEmpty(condition) ? true : condition == "test");
    }
}
