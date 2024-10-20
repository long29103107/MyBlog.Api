using Contracts.Abstractions.Common;
using Microsoft.EntityFrameworkCore;
using MyBlog.Identity.Domain.Entities;

namespace MyBlog.Identity.Repository.Abstractions;

public interface IRepositoryManager : IUnitOfWork<MyIdentityDbContext>
{
    public IUserRepository User { get; }
    public IRoleRepository Role { get; }
    public IPermissionRepository Permission { get; }

    DbSet<User> Users { get; }
    DbSet<Role> Roles { get; }
    DbSet<Permission> Permissions { get; }
}