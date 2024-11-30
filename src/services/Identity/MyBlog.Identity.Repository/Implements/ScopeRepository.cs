
using MyBlog.Identity.Domain.Entities;
using MyBlog.Identity.Repository.Abstractions;

namespace MyBlog.Identity.Repository.Implements;

public class ScopeRepository : RepositoryIdentityBase<Scope, MyIdentityDbContext>, IScopeRepository
{
    public ScopeRepository(MyIdentityDbContext context) : base(context)
    {
    }
}

