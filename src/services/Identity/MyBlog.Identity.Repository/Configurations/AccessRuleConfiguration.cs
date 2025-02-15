﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyBlog.Identity.Domain.Entities;

namespace MyBlog.Identity.Repository.Configurations;

public class AccessRuleConfiguration : IEntityTypeConfiguration<AccessRule>
{
    public void Configure(EntityTypeBuilder<AccessRule> builder)
    {
        // Configure relationships without cascading deletes
        builder.HasOne(x => x.Role)
            .WithMany(x => x.AccessRules)
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ur => ur.Permission)
            .WithMany(r => r.AccessRules)
            .HasForeignKey(ur => ur.PermissionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}