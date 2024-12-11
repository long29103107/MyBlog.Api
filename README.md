## Docker command line
```
docker-compose -f docker-compose.yml -f docker-compose.override.yml up
```

## Endpoint

- Gateway: http://localhost:5000
- Identity: http://localhost:5001
- Post: http://localhost:5002

## Migrations
|  Service  |  Method   |      Migration command																 |
|-----------|-----------|----------------------------------------------------------------------------------------|
|  Post     |  Add      | Add-migration Init -Context PostDbContext -o Post/Migrations							 | 
|  Post     |  Update   | Update-database -Context PostDbContext												 | 
|  Post     |  Script   | Script-Migration -Context PostDbContext												 | 
|  Identity |  Add      | Add-migration Init -Context MyIdentityDbContext -o Identity/Migrations	             | 
|  Identity |  Update   | Update-database -Context MyIdentityDbContext										     | 
|  Identity |  Script   | Script-Migration -Context MyIdentityDbContext											 | 

## Tech stack
```md
Common
├── Design-time DbContext factory
├── Entity framework
├── Dapper
├── Global error handling exception
├── Serilog
├── ValueOf
├── Audit entity
├── Autofac
├── Fluentvalidation
├── AutoMapper
├── Assembly reference
└── Unit of work
Category
├── Structure: 3 layers
├── Database: Postgres
└── Entity configuration
```