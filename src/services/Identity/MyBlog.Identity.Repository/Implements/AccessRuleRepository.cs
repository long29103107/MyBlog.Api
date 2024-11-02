
using MyBlog.Identity.Domain.Entities;
using MyBlog.Identity.Repository.Abstractions;

namespace MyBlog.Identity.Repository.Implements;

public class AccessRuleRepository : RepositoryIdentityBase<AccessRule, MyIdentityDbContext>, IAccessRuleRepository
{
    public AccessRuleRepository(MyIdentityDbContext context) : base(context)
    {
    }
}

