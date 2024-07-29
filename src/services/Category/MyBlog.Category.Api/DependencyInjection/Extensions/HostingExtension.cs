using Autofac;
using MyBlog.Shared.Serilog;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MyBlog.Category.Repository;
using MyBlog.Category.Repository.Implements;
using MyBlog.Category.Repository.Interfaces;
using MyBlog.Category.Service.MappingProfiles;
using MyBlog.Shared.Autofac.Modules;
using MyBlog.Shared.Middleware;
using MyBlog.Shared.RepositoryEF.DependencyInjection.Extensions;
using Serilog;
using Serilog.Exceptions;

namespace MyBlog.Category.Api.DependencyInjection.Extensions;

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
        builder.Services.AddScoped<ExceptionHandlingMiddleware>();
        builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
        builder.Services.AddDbContext<CategoryDbContext>(
          options => options.UseSqlServer(connectionString,
          b => b.MigrationsAssembly(AssemblyReference.AssemblyName)));

        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>((container) =>
            {
                container.RegisterModule(new GeneralModule<IRepositoryManager, RepositoryManager>(
                    MyBlog.Category.Service.AssemblyReference.Assembly,
                     MyBlog.Category.Repository.AssemblyReference.Assembly)
                );
            });
        builder.Services.AddGenericRepository();
        builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        return app; 
    }
}
