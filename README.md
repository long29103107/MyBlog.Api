## Docker command line
```
docker-compose -f docker-compose.yml -f docker-compose.override.yml up
```

## Endpoint

## Migrations

# Identity

// Configuration
Add-Migration Initial -Context ConfigurationDbContext -o Identity/Migrations/Configuration
Update-Database -Context ConfigurationDbContext

// PersistedGrant
Add-Migration Initial -Context PersistedGrantDbContext -o Identity/Migrations/PersistedGrant
Update-Database -Context PersistedGrantDbContext
Script-Migration -Context PersistedGrantDbContext

// LonGIdentityDb
Add-Migration Initial -Context LonGIdentityDbContext -o Identity/Migrations/LonG
Update-Database -Context LonGIdentityDbContext

dotnet ef migrations add Initial -c LonGIdentityDbContext -o Identity/Migrations/LonG -s ../../Identity/LonGBlog.Identity.Presentation/
dotnet ef database update -c LonGIdentityDbContext

Script-Migration -Context LonGIdentityDbContext 20240514111240_AddWeightRole

Add-Migration RemoveIsActiveRolePermission -Context LonGIdentityDbContext -o Identity/Migrations/LonG
