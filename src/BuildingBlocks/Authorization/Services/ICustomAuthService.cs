namespace Authorization.Services;

public interface ICustomAuthService
{
    Task<bool> CheckIfAllowed(string condition);
}

