using MyBlog.Shared.RepositoryEF.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace MyBlog.Shared.RepositoryEF.Implements;
public class RepositoryManagerBase<TContext>(IUnitOfWork<TContext> unitOfWork, TContext context) : IRepositoryManagerBase<TContext>
    where TContext : DbContext
{
    protected readonly IUnitOfWork<TContext> _unitOfWork = unitOfWork;
    protected readonly TContext _context = context;

    public Task<IDbContextTransaction> BeginTransaction()
    {
        return _context.Database.BeginTransactionAsync();
    }

    public Task EndTransactionAsync()
    {
        return _context.Database.CommitTransactionAsync();
    }

    public void RollbackTransaction()
    {
        _context.Database.RollbackTransactionAsync();
    }

    public Task<int> SaveAsync()
    {
        return _unitOfWork.CommitAsync();
    }
}
