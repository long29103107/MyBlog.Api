using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyBlog.Identity.Domain.Entities;
using System.Reflection.Emit;

namespace MyBlog.Identity.Repository.Configurations;

public class AccessRuleConfiguration : IEntityTypeConfiguration<AccessRule>
{
    public void Configure(EntityTypeBuilder<AccessRule> builder)
    {
        builder.HasOne(ar => ar.Permission)
            .WithMany()
            .HasForeignKey(ar => ar.PermissionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ar => ar.Role)
            .WithMany()
            .HasForeignKey(ar => ar.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}