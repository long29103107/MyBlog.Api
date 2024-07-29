using MyBlog.Shared.RepositoryEF.Implements;
using MyBlog.Shared.RepositoryEF.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MyBlog.Shared.RepositoryEF.DependencyInjection.Extensions;
public static class RepositoryServiceExtension
{
    public static IServiceCollection AddGenericRepository(this IServiceCollection services)
    {
        services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        services.AddScoped(typeof(IRepositoryManagerBase<>), typeof(RepositoryManagerBase<>));
        services.AddScoped(typeof(IRepositoryBase<,,>), typeof(RepositoryBase<,,>));

        return services;
    }
}
