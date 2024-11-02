using Contracts.Abstractions.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MyBlog.Identity.Domain.Entities;

namespace MyBlog.Identity.Repository.Abstractions;

public interface IRepositoryManager 
{
    //public IPermissionRepository Permission { get; }
    //public IOperationRepository Operation { get; }

    //DbSet<Permission> Permissions { get; }
    //DbSet<Operation> Operations { get; }

    //#region Transaction
    //Task SaveAsync();
    //Task<IDbContextTransaction> BeginTransactionAsync();
    //Task EndTransactionAsync();
    //Task RollbackTransactionAsync();
    //#endregion
}