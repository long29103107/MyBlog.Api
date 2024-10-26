using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.Extensions.Hosting;
using MyBlog.Identity.Repository.Abstractions;
using MyBlog.Identity.Repository.Implements;
using MyBlog.Shared.Autofac.Modules;
using MyBlog.Identity.Repository;
using Infrastructures.DependencyInjection.Extensions;
using MyBlog.Identity.Service.DependencyInjection.Options;
using Microsoft.Extensions.Configuration;

namespace MyBlog.Identity.Service;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServiceCollectionService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingletonOptions<JWT>(configuration, nameof(JWT));
        services.AddAutoMapper(IdentityServiceReference.Assembly);
        services.AddFluentValidation(v =>
        {
            v.ImplicitlyValidateChildProperties = true;
            v.ImplicitlyValidateRootCollectionElements = true;
            v.RegisterValidatorsFromAssembly(IdentityServiceReference.Assembly);
        });

        return services;
    }

    public static IHostBuilder AddHostService(this IHostBuilder builder)
    {
        builder.UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(container =>
            {
                container.RegisterModule(new GeneralModule<IRepositoryManager, RepositoryManager>(
                    IdentityServiceReference.Assembly,
                     IdentityRepositoryReference.Assembly)
                );
            });
        return builder;
    }
}