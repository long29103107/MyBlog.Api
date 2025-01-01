using FluentValidation.AspNetCore;
using MyBlog.Post.Service.Implements;
using MyBlog.Post.Service.Abstractions;
using MyBlog.Shared.ExceptionHandler;
using FilteringAndSortingExpression.Swagger.Extensions;
using System.Reflection;
using System.Xml.Linq;
using Authorization.Attributes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Contracts.Dtos;

namespace MyBlog.Post.Api.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServiceCollectionApi(this IServiceCollection services
        , IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddRouting(x => x.LowercaseUrls = true);
        services.AddControllers()
            .AddNewtonsoftJson();
        services.AddEndpointsApiExplorer();
        services.AddScoped<IScopedCache, ScopedCache>();
        services.AddSwagger(x =>
        {
            x.Name = PostApiReference.AssemblyName;
            x.Version = "v1";
            x.Title = PostApiReference.AssemblyName;
        });
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })

         // Adding Jwt Bearer
         .AddJwtBearer(options =>
         {
             options.SaveToken = true;
             options.RequireHttpsMetadata = false;
             options.TokenValidationParameters = new TokenValidationParameters()
             {
                 ValidateIssuer = true,
                 ValidateAudience = true,
                 ValidAudience = configuration["JWT:ValidAudience"],
                 ValidIssuer = configuration["JWT:ValidIssuer"],
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
             };
         });

        services.AddAuthorization();

        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }

    public static IHostBuilder AddHostApi(this IHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, config) =>
        {
            var env = context.HostingEnvironment;
            config.SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("sharedSettings.json", true, true)
                .AddJsonFile($"sharedSettings.{env.EnvironmentName}.json", true, true)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();
        });

        return builder;
    }
}