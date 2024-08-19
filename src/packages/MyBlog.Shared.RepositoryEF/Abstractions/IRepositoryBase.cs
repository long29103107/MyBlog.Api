using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MyBlog.Contracts.Domains;
using System.Linq.Expressions;

namespace MyBlog.Shared.RepositoryEF.Interfaces;

public interface IRepositoryBase<T, TContext, TKey>
    where T : EntityAuditBase<TKey>
    where TContext : DbContext
{
    #region Query
    IQueryable<T> FindAll(bool isTracking = false);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool isTracking = false);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>>[] expressions, bool isTracking = false);
    #endregion

    #region Action
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Update(T entity);
    void UpdateRange(IEnumerable<T> entities);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    #endregion

    #region Transaction
    Task<int> SaveChangesAsync();
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task EndTransactionAsync();
    Task RollbackTransactionAsync();
    #endregion

    #region Linq 
    bool Any();
    Task<bool> AnyAsync();
    #endregion
}
