using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyBlog.Identity.Domain.Entities;

namespace MyBlog.Identity.Repository;
public class MyIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    public MyIdentityDbContext(DbContextOptions<MyIdentityDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}

//public class MyIdentityDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
//{
//    public MyIdentityDbContext(DbContextOptions<MyIdentityDbContext> options) : base(options)
//    {
//    }

//    public virtual DbSet<User> Users { get; set; }
//    public virtual DbSet<Operation> Operations { get; set; }
//    public virtual DbSet<Permission> Permissions { get; set; }
//    public virtual DbSet<Role> Roles { get; set; }
//    public virtual DbSet<UserRole> UserRoles { get; set; }
//    public virtual DbSet<Domain.Entities.AccessRule> AccessRules { get; set; }

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        base.OnModelCreating(modelBuilder);

//        modelBuilder.ApplyConfigurationsFromAssembly(IdentityRepositoryReference.Assembly);
//        _UpdateNameTableIdentity(modelBuilder);
//        //modelBuilder.Ignore<IdentityRoleClaim<int>>();
//        //modelBuilder.Ignore<IdentityUserClaim<int>>();
//    }

//    private void _UpdateNameTableIdentity(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<IdentityUserLogin<int>>(entity => { entity.ToTable(IdentitySchemaConstants.Table.UserLogins); });

//        modelBuilder.Entity<IdentityUserClaim<int>>(entity => { entity.ToTable(IdentitySchemaConstants.Table.UserClaims); });

//        modelBuilder.Entity<IdentityRoleClaim<int>>(entity => { entity.ToTable(IdentitySchemaConstants.Table.RoleClaims); });

//        modelBuilder.Entity<IdentityUserToken<int>>(entity => { entity.ToTable(IdentitySchemaConstants.Table.UserTokens); });
//    }
//}