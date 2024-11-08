
using MyBlog.Identity.Domain.Entities;
using MyBlog.Identity.Repository.Abstractions;

namespace MyBlog.Identity.Repository.Implements;

public class OperationPermissionRepository : RepositoryIdentityBase<OperationPermission, MyIdentityDbContext>, IOperationPermissionRepository
{
    public OperationPermissionRepository(MyIdentityDbContext context) : base(context)
    {
    }
}

