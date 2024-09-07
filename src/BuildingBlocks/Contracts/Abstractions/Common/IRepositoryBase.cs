using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Contracts.Abstractions.Common;

public interface IRepositoryBase<T, TContext>
    where TContext : DbContext
{
    #region Query
    IQueryable<T> FindAll(bool isTracking = false);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool isTracking = false);
    Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, bool isTracking = false);
    T FirstOrDefault(Expression<Func<T, bool>> expression, bool isTracking = false);
    #endregion

    #region Action
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Update(T entity);
    void UpdateRange(IEnumerable<T> entities);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    #endregion

    #region Linq 
    bool Any(Expression<Func<T, bool>> expression);
    Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
    bool Any();
    Task<bool> AnyAsync();
    #endregion
}
