using Infrastructures.Common;
using MyBlog.Identity.Domain.Entities;
using MyBlog.Identity.Repository.Abstractions;

namespace MyBlog.Identity.Repository.Implements;

public class UserRepository : RepositoryBase<User, MyIdentityDbContext>, IUserRepository
{
    public UserRepository(MyIdentityDbContext context) : base(context)
    {
    }
}
