using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace MyBlog.Shared.RepositoryEF.Interfaces;
public interface IRepositoryManagerBase<TContext>
    where TContext : DbContext
{
    Task<int> SaveAsync();
    Task<IDbContextTransaction> BeginTransaction();
    Task EndTransactionAsync();
    void RollbackTransaction();
}
