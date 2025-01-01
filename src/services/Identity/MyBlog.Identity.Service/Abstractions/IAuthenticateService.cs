using Contracts.Abstractions.Common;
using MyBlog.Identity.Repository.Abstractions;
using Shared.Dtos.Identity.Authenticate;

namespace MyBlog.Identity.Service.Abstractions;

public interface IAuthenticateService : IBaseService<IRepositoryManager>
{
    Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
}

