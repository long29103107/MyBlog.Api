using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBlog.Post.Repository.Implements;
using MyBlog.Post.Repository.Abstractions;
using Persistence.Interceptors;
using Microsoft.Extensions.Hosting;
using Serilog;
using Infrastructures.DependencyInjection.Extensions;
using MyBlog.Shared.Serilog;
using Serilog.Exceptions;

namespace MyBlog.Post.Repository.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServiceCollectionRepository(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureDbContext(configuration);
        services.AddScoped<IRepositoryManager, RepositoryManager>();
        services.AddServiceInfrastructuresBuildingBlock();

        return services;
    }

    private static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddSingleton<UpdateAuditableEntitiesInterceptor>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddDbContext<PostDbContext>(
           (sp, options) => options.UseNpgsql(connectionString,
           b => b.MigrationsAssembly(PostRepositoryReference.AssemblyName))
            .AddInterceptors(sp.GetRequiredService<UpdateAuditableEntitiesInterceptor>()));
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