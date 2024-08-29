using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MyBlog.Post.Service.Implements;
using MyBlog.Post.Service.Interfaces;

namespace MyBlog.Post.Service.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServiceCollectionService(this IServiceCollection services)
    {
        services.AddScoped<IPostService, PostService>();
        services.AddFluentValidation(v =>
        {
            v.ImplicitlyValidateChildProperties = true;
            v.ImplicitlyValidateRootCollectionElements = true;
            v.RegisterValidatorsFromAssembly(PostServiceReference.Assembly);
        });

        return services;
    }
}