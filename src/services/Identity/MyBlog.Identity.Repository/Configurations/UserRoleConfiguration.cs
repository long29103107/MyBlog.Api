using Contracts.Domain.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyBlog.Identity.Domain.Entities;
using System.Reflection.Emit;

namespace MyBlog.Identity.Repository.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable(name: IdentitySchemaConstants.Table.UserRoles);

        //Configure the composite key for UserRole if needed
        builder.HasKey(ur => new { ur.UserId, ur.RoleId });

        // Configure relationships without cascading deletes
        builder.HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}


