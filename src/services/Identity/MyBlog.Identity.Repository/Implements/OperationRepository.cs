
using MyBlog.Identity.Domain.Entities;
using MyBlog.Identity.Repository.Abstractions;

namespace MyBlog.Identity.Repository.Implements;

public class OperationRepository : RepositoryIdentityBase<Operation, MyIdentityDbContext>, IOperationRepository
{
    public OperationRepository(MyIdentityDbContext context) : base(context)
    {
    }
}

