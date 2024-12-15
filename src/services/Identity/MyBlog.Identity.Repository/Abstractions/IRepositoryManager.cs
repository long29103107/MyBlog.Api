using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MyBlog.Identity.Domain.Entities;

namespace MyBlog.Identity.Repository.Abstractions;

public interface IRepositoryManager 
{
    public IPermissionRepository Permission { get; }
    public IOperationRepository Operation { get; }
    public IAccessRuleRepository AccessRule { get; }
    public IScopeRepository Scope { get; }
    public IRoleRepository Role { get; }
    public IUserRepository User { get; }
    public IUserRoleRepository UserRole { get; }

    DbSet<Permission> Permissions { get; }
    DbSet<Operation> Operations { get; }
    DbSet<AccessRule> AccessRules { get; }
    DbSet<Role> Roles { get; }
    DbSet<Scope> Scopes { get; }
    DbSet<User> Users { get; }
    DbSet<UserRole> UserRoles { get; }

    #region Transaction
    Task SaveAsync();
    void DetachEntities();
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task EndTransactionAsync();
    Task RollbackTransactionAsync();
    Task TruncateAsync(string tableName);
    #endregion
}