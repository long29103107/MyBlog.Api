using Infrastructures.Common;
using MyBlog.Identity.Domain.Entities;
using MyBlog.Identity.Repository.Abstractions;

namespace MyBlog.Identity.Repository.Implements;

public class RoleRepository : RepositoryBase<Role, MyIdentityDbContext>, IRoleRepository
{
    public RoleRepository(MyIdentityDbContext context) : base(context)
    {
    }
}
