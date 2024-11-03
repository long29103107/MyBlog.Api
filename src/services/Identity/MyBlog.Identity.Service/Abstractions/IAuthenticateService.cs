using Shared.Dtos.Identity.Authenticate;

namespace MyBlog.Identity.Service.Abstractions;

public interface IAuthenticateService
{
    Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
}

