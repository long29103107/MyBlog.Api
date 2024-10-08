﻿using Contracts.Domain;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Interceptors;

public class UpdateAuditableEntitiesInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        DbContext? dbContext = eventData.Context;

        if (dbContext is null)
            return base.SavingChangesAsync(eventData, result, cancellationToken);

        IEnumerable<EntityEntry<AuditEntity<int>>> entries = dbContext.ChangeTracker.Entries<AuditEntity<int>>();

        foreach (EntityEntry<AuditEntity<int>> entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(a => a.CreatedAt).CurrentValue = DateTime.UtcNow;
                entityEntry.Property(a => a.CreatedBy).CurrentValue = "Unknown";
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(a => a.UpdatedAt).CurrentValue = DateTime.UtcNow;
                entityEntry.Property(a => a.UpdatedBy).CurrentValue = "Unknown";
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}