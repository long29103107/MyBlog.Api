using Contracts.Abstractions.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MyBlog.Identity.Domain.Entities;
using MyBlog.Identity.Repository.Implements;

namespace MyBlog.Identity.Repository.Abstractions;

public interface IRepositoryManager 
{
    public IPermissionRepository Permission { get; }
    public IOperationRepository Operation { get; }
    public IAccessRuleRepository AccessRule { get; }
    public IOperationPermissionRepository OperationPermission { get; }
    public IRolePermissionRepository RolePermission { get; }

    DbSet<Permission> Permissions { get; }
    DbSet<Operation> Operations { get; }
    DbSet<AccessRule> AccessRules { get; }
    DbSet<OperationPermission> OperationPermissions { get; }
    DbSet<RolePermission> RolePermissions { get; }

    #region Transaction
    Task SaveAsync();
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task EndTransactionAsync();
    Task RollbackTransactionAsync();
    #endregion
}