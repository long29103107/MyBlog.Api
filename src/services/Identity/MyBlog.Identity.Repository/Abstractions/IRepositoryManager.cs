using Contracts.Abstractions.Common;

namespace MyBlog.Identity.Repository.Abstractions;

public interface IRepositoryManager : IUnitOfWork<MyIdentityDbContext>
{

}