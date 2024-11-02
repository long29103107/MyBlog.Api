using System.Linq.Expressions;
using Contracts.Abstractions.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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

    public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, bool isTracking = false)
    {
        return await FindByCondition(expression, isTracking).FirstOrDefaultAsync();
    }

    public T FirstOrDefault(Expression<Func<T, bool>> expression, bool isTracking = false)
    {
        return FindByCondition(expression, isTracking).FirstOrDefault();
    }

    public IQueryable<T> Include(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Include(expression);
    }
    public IQueryable<T> Includes(Expression<Func<T, bool>>[] expressions)
    {
        if(expressions.IsNullOrEmpty())
            return _context.Set<T>();

        foreach(var expression in expressions)
        {
            _context.Set<T>().Include(expression);
        }            

        return _context.Set<T>();
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
    public bool Any(Expression<Func<T, bool>> expression)
    {
        return Filter().Where(expression).Any();
    }
    public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
    {
        return await Filter().Where(expression).AnyAsync();
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();

    }
    public int Save()
    {
        return _context.SaveChanges();

    }

    public void Dispose()
    {
        _context.Dispose();

    }
    #endregion
}


public abstract class RepositoryIdentityBase<T, TContext, TUser, TRole, TUserRole> : IRepositoryIdentityBase<T, TContext, TUser, TRole, TUserRole>
    where T : class
    where TUser : IdentityUser<int>
    where TRole : IdentityRole<int>
    where TUserRole : IdentityUserRole<int>
    where TContext : IdentityDbContext<TUser, TRole, int, IdentityUserClaim<int>, TUserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
{
    protected readonly TContext _context;

    public RepositoryIdentityBase(TContext context)
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

    public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, bool isTracking = false)
    {
        return await FindByCondition(expression, isTracking).FirstOrDefaultAsync();
    }

    public T FirstOrDefault(Expression<Func<T, bool>> expression, bool isTracking = false)
    {
        return FindByCondition(expression, isTracking).FirstOrDefault();
    }

    public IQueryable<T> Include(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Include(expression);
    }
    public IQueryable<T> Includes(Expression<Func<T, bool>>[] expressions)
    {
        if (expressions.IsNullOrEmpty())
            return _context.Set<T>();

        foreach (var expression in expressions)
        {
            _context.Set<T>().Include(expression);
        }

        return _context.Set<T>();
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
    public bool Any(Expression<Func<T, bool>> expression)
    {
        return Filter().Where(expression).Any();
    }
    public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
    {
        return await Filter().Where(expression).AnyAsync();
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();

    }
    public int Save()
    {
        return _context.SaveChanges();

    }

    public void Dispose()
    {
        _context.Dispose();

    }
    #endregion
}
