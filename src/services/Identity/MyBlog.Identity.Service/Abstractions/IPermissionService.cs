using MyBlog.Identity.Domain.Entities;
using static Shared.Dtos.Identity.RoleDtos;

namespace MyBlog.Identity.Service.Abstractions;

public interface IPermissionService : IBaseIdentityService
{
    Task<RoleResponse> CreateAsync(RoleCreateRequest request);
    Task<RoleResponse> GetAsync(int id);
    Task<RoleResponse> GetActiveAsync(int id);
    Task<IEnumerable<RoleResponse>> GetListAsync(RoleListRequest request);
    Task<RoleResponse> UpdateAsync(int id, RoleUpdateRequest Role);
    Task<bool> DeleteAsync(int id);
}
