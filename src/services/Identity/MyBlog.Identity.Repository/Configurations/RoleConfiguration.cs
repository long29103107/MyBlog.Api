using Contracts.Domain.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBlog.Identity.Domain.Entities;

namespace MyBlog.Identity.Repository.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(name: IdentitySchemaConstants.Table.Roles);
        builder.HasIndex(c => c.Code)
           .IsUnique();
    }
}

