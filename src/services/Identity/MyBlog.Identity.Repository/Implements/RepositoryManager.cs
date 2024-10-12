using Contracts.Abstractions.Common;
using Infrastructures.Common;
using MyBlog.Identity.Repository.Abstractions;

namespace MyBlog.Identity.Repository.Implements;
public class RepositoryManager : UnitOfWork<MyIdentityDbContext>, IRepositoryManager
{
    public RepositoryManager(MyIdentityDbContext context) : base(context)
    {
    }
}
