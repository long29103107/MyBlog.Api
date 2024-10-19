using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Entities = MyBlog.Identity.Domain.Entities;

namespace MyBlog.Identity.Repository;

public class MyIdentityDbContext : IdentityDbContext<Entities.User>
{
    public MyIdentityDbContext(DbContextOptions<MyIdentityDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Entities.User> Users { get; set; }
    public virtual DbSet<Entities.Operation> Operations { get; set; }
    public virtual DbSet<Entities.Permission> Permissions { get; set; }
    public virtual DbSet<Entities.Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(IdentityRepositoryReference.Assembly);
        modelBuilder.Ignore<IdentityRoleClaim<int>>();
        modelBuilder.Ignore<IdentityUserClaim<int>>();
        modelBuilder.Ignore<IdentityRoleClaim<string>>();
        modelBuilder.Ignore<IdentityUserClaim<string>>();
    }
}