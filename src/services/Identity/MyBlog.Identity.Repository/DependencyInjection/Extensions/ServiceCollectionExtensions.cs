﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using MyBlog.Shared.Serilog;
using Serilog.Exceptions;
using Microsoft.Extensions.Logging;
using System;

namespace MyBlog.Identity.Repository;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServiceCollectionRepository(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddSerilog();
        });
        services.ConfigureDbContext(configuration);

        return services;
    }

    private static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<MyIdentityDbContext>(
        (sp, options) =>
        {
            var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
            options.UseSqlServer(connectionString,
                b => b.MigrationsAssembly(IdentityRepositoryReference.AssemblyName))
                .UseLoggerFactory(loggerFactory)
                .EnableSensitiveDataLogging();

        });
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