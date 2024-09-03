using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.Extensions.Hosting;
using MyBlog.Post.Repository.Abstractions;
using MyBlog.Post.Repository.Implements;
using MyBlog.Shared.Autofac.Modules;
using MyBlog.Post.Repository;

namespace MyBlog.Post.Service.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServiceCollectionService(this IServiceCollection services)
    {
        services.AddAutoMapper(PostServiceReference.Assembly);
        services.AddFluentValidation(v =>
        {
            v.ImplicitlyValidateChildProperties = true;
            v.ImplicitlyValidateRootCollectionElements = true;
            v.RegisterValidatorsFromAssembly(PostServiceReference.Assembly);
        });

        return services;
    }

    public static IHostBuilder AddHostService(this IHostBuilder builder)
    {
        builder.UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(container =>
            {
                container.RegisterModule(new GeneralModule<IRepositoryManager, RepositoryManager>(
                    PostServiceReference.Assembly,
                     PostRepositoryReference.Assembly)
                );
            });
        return builder;
    }
}