using MyBlog.Shared.ExceptionHandler;
using FilteringAndSortingExpression.Swagger.Extensions;
using MyBlog.Identity.Api;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MyBlog.Identity.Repository;

namespace MyBlog.Post.Api.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServiceCollectionApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddRouting(x => x.LowercaseUrls = true);
        services.AddControllers()
            .AddNewtonsoftJson();
        services.AddEndpointsApiExplorer();
        services.AddSwagger(x =>
        {
            x.Name = IdentityApiReference.AssemblyName;
            x.Version = "v1";
            x.Title = IdentityApiReference.AssemblyName;
        });
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }

    public static IHostBuilder AddHostApi(this IHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, config) =>
        {
            var env = context.HostingEnvironment;
            config.SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("sharedSettings.json", true, true)
                .AddJsonFile($"sharedSettings.{env.EnvironmentName}.json", true, true)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();
        });

        return builder;
    }
}