namespace MyBlog.Identity.Api.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServiceCollectionApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddControllers().AddNewtonsoftJson();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddRouting(x => x.LowercaseUrls = true);
            
        //services.AddExceptionHandler<GlobalExceptionHandler>();
        //services.AddProblemDetails();

        return services;
    }

    public static IHostBuilder AddHostApi(this IHostBuilder builder)
    { 

        var path = Directory.GetParent(AppContext.BaseDirectory).FullName;

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