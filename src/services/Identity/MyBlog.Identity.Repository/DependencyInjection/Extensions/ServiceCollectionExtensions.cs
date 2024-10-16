﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBlog.Identity.Repository.Implements;
using MyBlog.Identity.Repository.Abstractions;
using Persistence.Interceptors;
using Microsoft.Extensions.Hosting;
using Serilog;
using Infrastructures.DependencyInjection.Extensions;
using MyBlog.Shared.Serilog;
using Serilog.Exceptions;
using MyBlog.Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace MyBlog.Identity.Repository;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServiceCollectionRepository(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureDbContext(configuration);
        services.AddScoped<IRepositoryManager, RepositoryManager>();
        services.AddServiceInfrastructuresBuildingBlock();
        services.AddIdentityService();

        return services;
    }

    private static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddSingleton<UpdateAuditableEntitiesInterceptor>();
        services.AddDbContext<MyIdentityDbContext>(
           (sp, options) => options.UseNpgsql(connectionString,
           b => b.MigrationsAssembly(IdentityRepositoryReference.AssemblyName))
            .AddInterceptors(sp.GetRequiredService<UpdateAuditableEntitiesInterceptor>()));
    }

    public static IServiceCollection AddIdentityService(this IServiceCollection services)
    {
        services.AddIdentityCore<User>()
            .AddEntityFrameworkStores<MyIdentityDbContext>()
            .AddApiEndpoints();
        
        return services;
    }

    public static IHostBuilder AddHostRepository(this IHostBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .Enrich.WithMachineName()
            .WriteTo.Console()
            .CreateLogger();

        builder.UseSerilog((context, loggerConfig)
            => loggerConfig.ReadFrom.Configuration(context.Configuration));
        builder.ConfigureLogging(HostBuilderExtensions.ConfigureLogging);

        return builder;
    }
}