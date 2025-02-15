﻿using System.Linq.Expressions;

namespace MyBlog.Identity.Repository.Abstractions;
public interface IRepositoryIdentityBase<T, TContext>
    where T : class
    where TContext : MyIdentityDbContext
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
    void Detach(T entity);
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
