using MyBlog.Identity.Domain.Entities;
using static Shared.Dtos.Identity.Permission.PermissionDtos;
using static Shared.Dtos.Identity.RoleDtos;

namespace MyBlog.Identity.Service.Abstractions;

public interface IRoleService : IBaseIdentityService
{
    IQueryable<Role> _RoleIgnoreGlobalFilter();
    Task<RoleResponse> CreateAsync(RoleCreateRequest request);
    Task<RoleResponse> GetAsync(int id);
    Task<RoleResponse> GetActiveAsync(int id);
    Task<IEnumerable<RoleResponse>> GetListAsync(RoleListRequest request);
    Task<RoleResponse> UpdateAsync(int id, RoleUpdateRequest request);
    Task<bool> DeleteAsync(int id);
    
    Task<IEnumerable<PermissionResponse>> GetPermissionsByRoleAsync(int roleId);
    Task<PermissionResponse> GetPermissionByRoleAsync(int roleId, int permissionId);
}
