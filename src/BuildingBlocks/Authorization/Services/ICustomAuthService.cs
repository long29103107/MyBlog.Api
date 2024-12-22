namespace Authorization.Services;

public interface ICustomAuthService
{
    Task<bool> CheckIfAllowedAsync(int userId, string scope, string operation);
}

