using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyBlog.Identity.Domain.Entities;

namespace MyBlog.Identity.Repository.Configurations;

public class OperationPermissionConfiguration : IEntityTypeConfiguration<OperationPermission>
{
    public void Configure(EntityTypeBuilder<OperationPermission> builder)
    {
        builder.HasIndex(op => new { op.OperationId, op.PermissionId }).IsUnique();

        builder.HasOne(op => op.Operation)
            .WithMany(o => o.OperationPermissions)
            .HasForeignKey(op => op.OperationId)
            .OnDelete(DeleteBehavior.Cascade); // Khi Operation bị xóa, OperationPermission cũng bị xóa

        builder.HasOne(op => op.Permission)
            .WithMany(p => p.OperationPermissions)
            .HasForeignKey(op => op.PermissionId)
            .OnDelete(DeleteBehavior.Cascade); // Khi Permission bị xóa, OperationPermission cũng bị xóa
    }
}