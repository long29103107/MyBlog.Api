using Authorization.Attributes;
using Authorization.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyBlog.Identity.Domain.Entities;
using MyBlog.Identity.Repository;
using MyBlog.Identity.Service;
using MyBlog.Shared.Serilog;
using Serilog;
using System.Security.Claims;
using System.Text;

namespace MyBlog.Identity.Api.DependencyInjection.Extensions;

public static class HostingExtension
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

        services.AddScoped<ICustomAuthService, CustomAuthService>();

        builder.Host.AddHostApi()
            .AddHostRepository()
            .AddHostService();

        // For Entity Framework
        services.AddServiceCollectionApi(configuration)
            .AddServiceCollectionRepository(configuration)
            .AddServiceCollectionService(configuration);

        // For Identity
        services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<MyIdentityDbContext>()
            .AddDefaultTokenProviders();

        // Adding Authentication
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
                ValidAudience = builder.Configuration["JWT:ValidAudience"],
                ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
            };
        });

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseExceptionHandler();
        app.UseSerilogMiddleware();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseSerilogRequestLogging();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }
}
