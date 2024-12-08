using static Shared.Dtos.Identity.Permission.PermissionDtos;

namespace MyBlog.Identity.Service.Abstractions;

public interface IPermissionService : IBaseIdentityService
{
    Task<PermissionResponse> GetAsync(int id);
    Task<IEnumerable<PermissionResponse>> GetListAsync(PermissionListRequest request);
}
