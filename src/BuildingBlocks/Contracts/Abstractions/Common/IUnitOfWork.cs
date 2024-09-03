using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Contracts.Abstractions.Common;

public interface IUnitOfWork<TContext> : IDisposable
    where TContext : DbContext
{
    #region Transaction
    Task SaveAsync();
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task EndTransactionAsync();
    Task RollbackTransactionAsync();
    #endregion
}
