using Contracts.Domain.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyBlog.Identity.Domain.Entities;

namespace MyBlog.Identity.Repository;

public class MyIdentityDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
{
    public MyIdentityDbContext(DbContextOptions<MyIdentityDbContext> options) : base(options)
    {
       
    }

    public virtual DbSet<Permission> Permissions { get; set; }
    public virtual DbSet<Operation> Operations { get; set; }
    public virtual DbSet<AccessRule> AccessRules { get; set; }
    public virtual DbSet<RolePermission> RolePermissions { get; set; }
    public virtual DbSet<Scope> Scopes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(IdentityRepositoryReference.Assembly);

        builder.Entity<IdentityRoleClaim<int>>().ToTable(name: IdentitySchemaConstants.Table.RoleClaims);
        builder.Entity<IdentityUserClaim<int>>().ToTable(name: IdentitySchemaConstants.Table.UserClaims);
        builder.Entity<IdentityUserLogin<int>>().ToTable(name: IdentitySchemaConstants.Table.UserLogins);
        builder.Entity<IdentityUserToken<int>>().ToTable(name: IdentitySchemaConstants.Table.UserTokens);
    }
}
