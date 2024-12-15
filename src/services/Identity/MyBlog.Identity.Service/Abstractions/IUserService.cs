using static Shared.Dtos.Identity.UserDtos;

namespace MyBlog.Identity.Service.Abstractions;

public interface IUserService : IBaseIdentityService
{
    Task<UserResponse> GetAsync(int id);
    Task<UserResponse> GetActiveAsync(int id);
    Task<IEnumerable<UserResponse>> GetListAsync(UserListRequest request);
    Task<UserResponse> UpdateAsync(int id, UserUpdateRequest request);
    Task<bool> DeleteAsync(int id);
    Task AssignRoleAsync(int userId);
    //Task<bool> HasPermissionAsync(int userId);
}
