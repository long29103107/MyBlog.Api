using Shared.Dtos.Identity.Register;
using static Shared.Dtos.Identity.UserDtos;

namespace MyBlog.Identity.Service.Abstractions;

public interface IRegisterService : IBaseIdentityService
{
    Task<UserResponse> RegisterAsync(RegisterRequest request);
}
