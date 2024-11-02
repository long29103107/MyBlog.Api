using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        //Configure the composite key for UserRole if needed
        builder.Entity<UserRole>()
            .HasKey(ur => new { ur.UserId, ur.RoleId });

        // Configure relationships without cascading deletes
        builder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<UserRole>()
            .HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ApplyConfigurationsFromAssembly(IdentityRepositoryReference.Assembly);
    }
}
