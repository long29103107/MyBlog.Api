using Microsoft.Extensions.DependencyInjection;

namespace LonGBlog.Shared.Contract.Caches;

public static class ServiceExtension
{
    public static IServiceCollection ConfigueCache(this IServiceCollection services)
    {
        services.AddScoped<IScopedCache, ScopedCache>();

        return services;
    }

}