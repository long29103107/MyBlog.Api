using Microsoft.EntityFrameworkCore;
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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MyBlog.Identity.Repository.Stores;

namespace MyBlog.Identity.Repository;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServiceCollectionRepository(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureDbContext(configuration);
        services.AddServiceInfrastructuresBuildingBlock();
        services.AddIdentityService(configuration);

        return services;
    }

    private static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddSingleton<UpdateAuditableEntitiesInterceptor>();
        services.AddDbContext<MyIdentityDbContext>(
           (sp, options) => options.UseSqlServer(connectionString,
           b => b.MigrationsAssembly(IdentityRepositoryReference.AssemblyName))
            .AddInterceptors(sp.GetRequiredService<UpdateAuditableEntitiesInterceptor>()));
    }

    public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<MyIdentityDbContext>()
            .AddUserStore<UserStore>()
            .AddRoleStore<RoleStore>()
            .AddApiEndpoints();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = configuration["JWT:ValidAudience"],
                ValidIssuer = configuration["JWT:ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
            };
        });

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