using FastEndpoints;
using FastEndpoints.Swagger;
using MyBlog.Post.Repository;
using Microsoft.EntityFrameworkCore;
using Autofac;
using MyBlog.Post.Repository.Interfaces;
using MyBlog.Post.Repository.Implements;
using MyBlog.Post.Service.MappingProfile;
using FluentValidation.AspNetCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace MyBlog.Post.Endpoint.DependencyInjection.Extensions;

public static class HostingExtension
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        //Add contoller, swagger, httpcontext, routing
        //builder.Services.ConfigureCommonServiceCollection();
        builder.Services.AddControllers(config =>
        {
            config.RespectBrowserAcceptHeader = true;
            config.ReturnHttpNotAcceptable = true;
            config.Filters.Add(new ProducesAttribute("application/json", "text/plain", "text/json"));
        }).AddNewtonsoftJson();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddRouting(x => x.LowercaseUrls = true);

        //Add serilog and app config
        //builder.Host.ConfigureHostSerilogMiddleware();
        Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Enrich.WithMachineName()
            .WriteTo.Console()
            .CreateLogger();


        builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));
        builder.Host.ConfigureLogging(HostBuilderExtensions.ConfigureLogging);
        builder.Host.ConfigureAppConfiguration((context, config) =>
        {
            var env = context.HostingEnvironment;
            config.AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();
        });

        //Add autofac, serilog, automapper, dbcontext, fluentvalidation,swagger
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>((container) =>
            {
                container.RegisterModule(new GeneralModule<IRepositoryManager, RepositoryManager>(
                    MyBlog.Post.Service.AssemblyReference.Assembly,
                    MyBlog.Post.Repository.AssemblyReference.Assembly)
                );
            });

        builder.Services.AddSerilogMiddleware(true);
        builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
        builder.Services.AddDbContext<PostDbContext>(
            options => options.UseSqlServer(connectionString,
            b => b.MigrationsAssembly(AssemblyReference.AssemblyName)));

        builder.Services.AddFluentValidation(v =>
        {
            v.ImplicitlyValidateChildProperties = true;
            v.ImplicitlyValidateRootCollectionElements = true;
            v.RegisterValidatorsFromAssembly(MyBlog.Post.Service.AssemblyReference.Assembly);
        });

        builder.Services.SwaggerDocument(o =>
        {
            o.DocumentSettings = s =>
            {
                s.Title = AssemblyReference.AssemblyName;
                s.DocumentName = AssemblyReference.AssemblyName;
                s.Version = "v1";
            };
            o.AutoTagPathSegmentIndex = 0;
        });

        //Add lifetime 
        ////builder.Services.AddGenericRepository();
        //builder.Services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        //builder.Services.AddScoped(typeof(IRepositoryManagerBase<>), typeof(RepositoryManagerBase<>));
        //builder.Services.AddScoped(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>));

        //Add dependecy for specific project
        builder.Services.AddFastEndpoints();
        //builder.Services.AddAuthentication(options =>
        //{
        //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        //});

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        //app.UseSerilogMiddleware();
        //app.UseSerilogRequestLogging();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseFastEndpoints(c =>
        {
            c.Serializer.ResponseSerializer = (rsp, dto, cType, jCtx, ct) =>
            {
                var settings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    Converters = new List<JsonConverter> { new StringEnumConverter() }
                };
                rsp.ContentType = cType;

                return rsp.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(dto, settings), ct);
            };
        });

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger(c => c.RouteTemplate = "/swagger/{documentName}/swagger.json");
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Song API"));
        }

        return app;
    }
}
;