using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Contracts.Abstractions.Common;

public interface IRepositoryBase<T, TContext>
    where T : class
    where TContext : DbContext
{
    #region Query
    IQueryable<T> FindAll(bool isTracking = false);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool isTracking = false);
    Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, bool isTracking = false);
    T FirstOrDefault(Expression<Func<T, bool>> expression, bool isTracking = false);
    IQueryable<T> Include(Expression<Func<T, bool>> expression);
    IQueryable<T> Includes(Expression<Func<T, bool>>[] expressions);
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
    Task<int> SaveAsync();
    int Save();
    void Dispose();
    #endregion
}

public interface IRepositoryIdentityBase<T, TContext, TUser, TRole, TUserRole>
    where T : class
    where TUser : IdentityUser<int>
    where TRole : IdentityRole<int>
    where TUserRole : IdentityUserRole<int>
    where TContext : IdentityDbContext<TUser, TRole, int, IdentityUserClaim<int>, TUserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
{
    #region Query
    IQueryable<T> FindAll(bool isTracking = false);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool isTracking = false);
    Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, bool isTracking = false);
    T FirstOrDefault(Expression<Func<T, bool>> expression, bool isTracking = false);
    IQueryable<T> Include(Expression<Func<T, bool>> expression);
    IQueryable<T> Includes(Expression<Func<T, bool>>[] expressions);
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
    Task<int> SaveAsync();
    int Save();
    void Dispose();
    #endregion
}
