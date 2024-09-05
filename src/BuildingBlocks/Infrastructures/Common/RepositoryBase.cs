using System.Linq.Expressions;
using Contracts.Abstractions.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructures.Common;

public abstract class RepositoryBase<T, TContext> : IRepositoryBase<T, TContext>
    where T : class
    where TContext : DbContext
{
    protected readonly TContext _context;

    public RepositoryBase(TContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(_context));
    }

    #region Filter
    public virtual IQueryable<T> Filter()
    {
        return _context.Set<T>();
    }
    #endregion

    #region Query
    public IQueryable<T> FindAll(bool isTracking = false)
    {
        if (isTracking)
        {
            return Filter();
        }

        return Filter().AsNoTracking();
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool isTracking = false)
    {
        if (isTracking)
        {
            return Filter().Where(expression);
        }

        return Filter().AsNoTracking().Where(expression);
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>>[] expressions, bool isTracking = false)
    {
        var queryable = Filter();

        if (!isTracking)
        {
            queryable = queryable.AsNoTracking();
        }

        if(!expressions.IsNullOrEmpty())
        {
            foreach (var expression in expressions)
            {
                queryable = queryable.Where(expression);
            }
        }

        return queryable;
    }

    public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, bool isTracking = false)
    {
        return await FindByCondition(expression, isTracking).FirstOrDefaultAsync();
    }

    public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>>[] expressions, bool isTracking = false)
    {
        return await FindByCondition(expressions, isTracking).FirstOrDefaultAsync();
    }

    public T FirstOrDefault(Expression<Func<T, bool>>[] expressions, bool isTracking = false)
    {
        return FindByCondition(expressions, isTracking).FirstOrDefault();
    }
    public T FirstOrDefault(Expression<Func<T, bool>> expression, bool isTracking = false)
    {
        return FindByCondition(expression, isTracking).FirstOrDefault();
    }
    #endregion

    #region Action
    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }
    public void AddRange(IEnumerable<T> entities)
    {
        _context.Set<T>().AddRange(entities);
    }
    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }
    public void UpdateRange(IEnumerable<T> entities)
    {
        _context.Set<T>().UpdateRange(entities);
    }
    public void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
    }
    public void RemoveRange(IEnumerable<T> entities)
    {
        if (entities == null)
            return;

        _context.Set<T>().RemoveRange(entities);
    }
    #endregion

    #region Linq 
    public bool Any()
    {
        return Filter().Any();
    }
    public async Task<bool> AnyAsync()
    {
        return await Filter().AnyAsync();
    }
    #endregion
}
