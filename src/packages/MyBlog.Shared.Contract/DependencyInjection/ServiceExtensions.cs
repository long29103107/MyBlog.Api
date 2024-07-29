using LonGBlog.Shared.Serilog;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LonGBlog.Shared.Contract.DependencyInjection;

public static class ServiceExtensions
{
    public static IServiceCollection ConfigureCommonServiceCollection(this IServiceCollection services)
    {
        //Add contoller, swagger, httpcontext, routing
        services.AddControllers(config =>
        {
            config.RespectBrowserAcceptHeader = true;
            config.ReturnHttpNotAcceptable = true;
            config.Filters.Add(new ProducesAttribute("application/json", "text/plain", "text/json"));
        }).AddNewtonsoftJson();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddHttpContextAccessor();
        services.AddRouting(x => x.LowercaseUrls = true);

        return services;
    }

    public static void ConfigureCommonHost(this ConfigureHostBuilder host)
    {
        host.ConfigureHostSerilogMiddleware();
        host.ConfigureAppConfiguration((context, config) =>
        {
            var env = context.HostingEnvironment;
            config.AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();
        });
    }
}