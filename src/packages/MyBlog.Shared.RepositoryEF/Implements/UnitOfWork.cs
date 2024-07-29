using MyBlog.Shared.RepositoryEF.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MyBlog.Shared.RepositoryEF.Implements;
public class UnitOfWork<TContext> : IUnitOfWork<TContext>
    where TContext : DbContext
{
    private readonly TContext _context;

    public UnitOfWork(TContext context)
    {
        _context = context;
    }

    public Task<int> CommitAsync()
    {
        return _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task SaveChangeAsync()
    {
        await _context.SaveChangesAsync();
    }
}