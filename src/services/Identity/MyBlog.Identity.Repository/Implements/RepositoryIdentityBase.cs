using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyBlog.Identity.Repository.Abstractions;
using System.Linq.Expressions;

namespace MyBlog.Identity.Repository.Implements;

public abstract class RepositoryIdentityBase<T, TContext> 
    : IRepositoryIdentityBase<T, TContext>
    where T : class
    where TContext : MyIdentityDbContext
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

    public void Detach(T entity)
    {
        _context.Entry(entity).State = EntityState.Detached;
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
