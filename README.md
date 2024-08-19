## Docker command line
```
docker-compose -f docker-compose.yml -f docker-compose.override.yml up
```

## Endpoint

- Gateway: http://localhost:5000
- Identity: http://localhost:5001
- Category: http://localhost:5002
- Post: http://localhost:5003

## Migrations
|  Service  |  Method   |      Migration command                                                      |
|-----------|-----------|-----------------------------------------------------------------------------|
|  Category |  Add      | Add-migration Init -Context CategoryDbContext -o Category/Migrations        | 
|  Category |  Update   | Update-database -Context CategoryDbContext                                  | 
|  Category |  Script   | Script-Migration -Context CategoryDbContext                                 | 

## Tech stack
```md
├── Common
│   ├── Design-time DbContext factory
│   ├── Entity framework
│   ├── Dapper
│   ├── Global error handling exception
│   ├── Serilog
│   ├── ValueOf
│   ├── Audit entity
│   ├── Autofac
│   ├── Fluentvalidation
│   ├── AutoMapper
│   ├── Assembly reference
│   └── Unit of work
├── Category
	├── Structure: 3 layers
	├── Database: Postgres
	└── Entity configuration
```