using Microsoft.Extensions.DependencyInjection;
using MyBlog.Post.Repository.Implements;
using MyBlog.Post.Repository.Interfaces;

namespace MyBlog.Post.Repository.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServiceCollectionRepositoy(this IServiceCollection services)
    {
        services.AddScoped<IPostRepository, PostRepository>();

        return services;
    }
}