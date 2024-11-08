
using MyBlog.Identity.Domain.Entities;
using MyBlog.Identity.Repository.Abstractions;

namespace MyBlog.Identity.Repository.Implements;

public class RolePermissionRepository : RepositoryIdentityBase<RolePermission, MyIdentityDbContext>, IRolePermissionRepository
{
    public RolePermissionRepository(MyIdentityDbContext context) : base(context)
    {
    }
}

