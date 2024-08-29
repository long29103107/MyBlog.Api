using MyBlog.Shared.Serilog;
using Microsoft.EntityFrameworkCore;
using MyBlog.Shared.RepositoryEF.DependencyInjection.Extensions;
using Serilog;
using Serilog.Exceptions;
using MyBlog.Shared.ExceptionHandler;
using MyBlog.Post.Repository.Interfaces;
using MyBlog.Post.Repository.Implements;
using MyBlog.Post.Repository;
using MyBlog.Post.Service.MappingProfiles;
using MyBlog.Post.Repository.DependencyInjection.Extensions;
using MyBlog.Post.Service.DependencyInjection.Extensions;

namespace MyBlog.Post.Api.DependencyInjection.Extensions;

public static class HostingExtension
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        Log.Logger = new LoggerConfiguration()
           .MinimumLevel.Information()
           .Enrich.FromLogContext()
           .Enrich.WithExceptionDetails()
           .Enrich.WithMachineName()
           .WriteTo.Console()
           .CreateLogger();

        builder.Host.UseSerilog((context, loggerConfig)
            => loggerConfig.ReadFrom.Configuration(context.Configuration));
        builder.Host.ConfigureLogging(HostBuilderExtensions.ConfigureLogging);

        builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
        builder.Services.AddServiceCollectionRepositoy()
            .AddServiceCollectionService();

        builder.Host.ConfigureAppConfiguration((context, config) =>
        {
            var env = context.HostingEnvironment;
            config.SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("sharedSettings.json", true, true)
                .AddJsonFile($"sharedSettings.{env.EnvironmentName}.json", true, true)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();
        });

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddRouting(x => x.LowercaseUrls = true);
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

        builder.Services.AddDbContext<PostDbContext>(
          options => options.UseNpgsql(connectionString,
          b => b.MigrationsAssembly(PostApiReference.AssemblyName)));

        //builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
        //    .ConfigureContainer<ContainerBuilder>((container) =>
        //    {
        //        container.RegisterModule(new GeneralModule<IRepositoryManager, RepositoryManager>(
        //            PostServiceReference.Assembly,
        //             PostRepositoryReference.Assembly)
        //        );
        //    });
        builder.Services.AddGenericRepository();
        builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
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
