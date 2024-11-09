using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyBlog.Identity.Domain.Entities;

namespace MyBlog.Identity.Repository.Configurations;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasIndex(op => new { op.RoleId, op.PermissionId }).IsUnique();

        builder.HasOne(op => op.Role)
            .WithMany(o => o.RolePermissions)
            .HasForeignKey(op => op.RoleId)
            .OnDelete(DeleteBehavior.Cascade); // Khi Role bị xóa, RolePermission cũng bị xóa

        builder.HasOne(op => op.Permission)
            .WithMany(p => p.RolePermissions)
            .HasForeignKey(op => op.PermissionId)
            .OnDelete(DeleteBehavior.Cascade); // Khi Permission bị xóa, RolePermission cũng bị xóa
    }
}