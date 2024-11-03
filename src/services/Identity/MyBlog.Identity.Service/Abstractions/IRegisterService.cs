using Shared.Dtos.Identity.Register;

namespace MyBlog.Identity.Service.Abstractions;

public interface IRegisterService
{
    Task<RegisterResponse> RegisterAsync(RegisterRequest request);
}
