using MyBlog.Shared.Serilog;
using Serilog;
using Serilog.Exceptions;
using MyBlog.Post.Repository.DependencyInjection.Extensions;
using MyBlog.Post.Service.DependencyInjection.Extensions;

namespace MyBlog.Post.Api.DependencyInjection.Extensions;

public static class HostingExtension
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        //Add service
        builder.Services
            .AddServiceCollectionApi()
            .AddServiceCollectionRepository(builder.Configuration)
            .AddServiceCollectionService();

        //Add host
        builder.Host
            .AddHostApi()
            .AddHostService()
            .AddHostRepository();

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

        app.MapControllers();

        return app;
    }
}
