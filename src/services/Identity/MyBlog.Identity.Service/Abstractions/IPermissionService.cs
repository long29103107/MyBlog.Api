using Contracts.Abstractions.Common;
using MyBlog.Identity.Repository.Abstractions;
using static Shared.Dtos.Identity.Permission.PermissionDtos;

namespace MyBlog.Identity.Service.Abstractions;

public interface IPermissionService : IBaseService<IRepositoryManager>
{
    Task<PermissionResponse> GetAsync(int id);
    Task<IEnumerable<PermissionResponse>> GetListAsync(PermissionListRequest request);
    Task<List<PermissionGrpListByRoleResponse>> GetPermissionByRoleIdAsync(PermissionListByRoleRequest request);
    Task<bool> HasPermissionAsync(int userId, string permission);
    Task<bool> HasPermissionAsync(int userId, int permissionId);
}
