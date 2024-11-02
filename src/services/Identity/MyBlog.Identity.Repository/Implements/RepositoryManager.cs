using MyBlog.Identity.Repository.Abstractions;

namespace MyBlog.Identity.Repository.Implements;
public class RepositoryManager : IRepositoryManager
{
    private readonly MyIdentityDbContext _context;

    public RepositoryManager(MyIdentityDbContext context)
    {
        _context = context;
    }

    //#region Transaction
    //public void Dispose()
    //{
    //    _context.Dispose();
    //}

    //public async Task SaveAsync()
    //{
    //    await _context.SaveChangesAsync();
    //}
    //public Task<IDbContextTransaction> BeginTransactionAsync()
    //{
    //    return _context.Database.BeginTransactionAsync();
    //}

    //public async Task EndTransactionAsync()
    //{
    //    await SaveAsync();
    //    await _context.Database.CommitTransactionAsync();
    //}

    //public async Task RollbackTransactionAsync()
    //{
    //    await _context.Database.RollbackTransactionAsync();
    //}
    //#endregion

    //private IPermissionRepository _permission;
    //private IOperationRepository _operation;

    //public IPermissionRepository Permission
    //{
    //    get
    //    {
    //        if (_permission == null)
    //        {
    //            _permission = new PermissionRepository(_context);
    //        }

    //        return _permission;
    //    }
    //}
    //public IOperationRepository Operation
    //{
    //    get
    //    {
    //        if (_operation == null)
    //        {
    //            _operation = new OperationRepository(_context);
    //        }

    //        return _operation;
    //    }
    //}

    //public DbSet<Operation> Operations { get { return _context.Operations; } }
    //public DbSet<Permission> Permissions { get { return _context.Permissions; } }
}