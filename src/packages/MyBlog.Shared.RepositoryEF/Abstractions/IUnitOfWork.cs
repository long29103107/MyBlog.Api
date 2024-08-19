using Microsoft.EntityFrameworkCore;

namespace MyBlog.Shared.RepositoryEF.Interfaces;
public interface IUnitOfWork<TContext> : IDisposable
    where TContext : DbContext
{
    Task<int> CommitAsync();
    Task SaveChangeAsync();
}
