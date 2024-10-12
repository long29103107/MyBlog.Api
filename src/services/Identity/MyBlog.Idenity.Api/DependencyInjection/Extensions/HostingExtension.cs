using MyBlog.Identity.Domain.Entities;
using MyBlog.Identity.Repository;
using MyBlog.Identity.Service;
using MyBlog.Post.Api.DependencyInjection.Extensions;

namespace MyBlog.Identity.Api.DependencyInjection.Extensions;

public static class HostingExtension
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        //Add service
        builder.Services
            .AddServiceCollectionApi()
            .AddServiceCollectionRepository(builder.Configuration);
            //.AddServiceCollectionService();

        //Add host
        builder.Host
            .AddHostApi()
            .AddHostRepository(); ;
            //.AddHostService();

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseExceptionHandler();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapIdentityApi<User>();

        app.MapControllers();

        return app;
    }
}
