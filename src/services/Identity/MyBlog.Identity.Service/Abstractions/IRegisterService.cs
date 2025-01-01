using Contracts.Abstractions.Common;
using MyBlog.Identity.Repository.Abstractions;
using Shared.Dtos.Identity.Register;
using static Shared.Dtos.Identity.UserDtos;

namespace MyBlog.Identity.Service.Abstractions;

public interface IRegisterService : IBaseService<IRepositoryManager>
{
    Task<UserResponse> RegisterAsync(RegisterRequest request);
}
