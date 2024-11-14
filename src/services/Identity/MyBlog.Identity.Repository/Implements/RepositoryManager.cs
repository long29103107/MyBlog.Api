using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MyBlog.Identity.Domain.Entities;
using MyBlog.Identity.Repository.Abstractions;

namespace MyBlog.Identity.Repository.Implements;
public class RepositoryManager : IRepositoryManager
{
    private readonly MyIdentityDbContext _context;

    public RepositoryManager(MyIdentityDbContext context)
    {
        _context = context;
    }

    #region Transaction
    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void DetachEntities()
    {
        _context.ChangeTracker.Clear();
    }

    public Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return _context.Database.BeginTransactionAsync();
    }

    public async Task EndTransactionAsync()
    {
        await SaveAsync();
        await _context.Database.CommitTransactionAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await _context.Database.RollbackTransactionAsync();
    }
    public async Task TruncateAsync(string tableName)
    {
        await _context.Database.ExecuteSqlRawAsync($"DELETE FROM {tableName}");
    }
    #endregion

    private IPermissionRepository _permission;
    private IOperationRepository _operation;
    private IAccessRuleRepository _accessRule;
    private IOperationPermissionRepository _operationPermission;
    private IRolePermissionRepository _rolePermission;
    private IRoleRepository _role;

    public IPermissionRepository Permission
    {
        get
        {
            if (_permission == null)
            {
                _permission = new PermissionRepository(_context);
            }

            return _permission;
        }
    }

    public IOperationRepository Operation
    {
        get
        {
            if (_operation == null)
            {
                _operation = new OperationRepository(_context);
            }

            return _operation;
        }
    }
    public IAccessRuleRepository AccessRule
    {
        get
        {
            if (_accessRule == null)
            {
                _accessRule = new AccessRuleRepository(_context);
            }

            return _accessRule;
        }
    }

    public IOperationPermissionRepository OperationPermission
    {
        get
        {
            if (_operationPermission == null)
            {
                _operationPermission = new OperationPermissionRepository(_context);
            }

            return _operationPermission;
        }
    }

    public IRolePermissionRepository RolePermission
    {
        get
        {
            if (_rolePermission == null)
            {
                _rolePermission = new RolePermissionRepository(_context);
            }

            return _rolePermission;
        }
    }

    public IRoleRepository Role
    {
        get
        {
            if (_role == null)
            {
                _role = new RoleRepository(_context);
            }

            return _role;
        }
    }

    public DbSet<Operation> Operations { get { return _context.Operations; } }
    public DbSet<Permission> Permissions { get { return _context.Permissions; } }
    public DbSet<AccessRule> AccessRules { get { return _context.AccessRules; } }
    public DbSet<RolePermission> RolePermissions { get { return _context.RolePermissions; } }
    public DbSet<OperationPermission> OperationPermissions { get { return _context.OperationPermissions; } }
    public DbSet<Role> Roles { get { return _context.Roles; } }
}