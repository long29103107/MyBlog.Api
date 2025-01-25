using MyBlog.Shared.Serilog;
using Serilog;
using Serilog.Exceptions;
using MyBlog.Post.Repository.DependencyInjection.Extensions;
using MyBlog.Post.Service.DependencyInjection.Extensions;
using Authorization.Services;
using Autofac.Core;

namespace MyBlog.Post.Api.DependencyInjection.Extensions;

public static class HostingExtension
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICustomAuthService, CustomAuthService>();

        //Add host
        builder.Host
            .AddHostApi()
            .AddHostService()
            .AddHostRepository();

        //Add service
        builder.Services
            .AddServiceCollectionApi(builder.Configuration)
            .AddServiceCollectionRepository(builder.Configuration)
            .AddServiceCollectionService();

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

        app.UseRouting();
        //app.UseCors();
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}
