using MyBlog.Identity.Domain.Entities;
using MyBlog.Identity.Repository.Abstractions;

namespace MyBlog.Identity.Repository.Implements;

public class UserRoleRepository : RepositoryIdentityBase<UserRole, MyIdentityDbContext>, IUserRoleRepository
{
    public UserRoleRepository(MyIdentityDbContext context) : base(context)
    {
    }
}

