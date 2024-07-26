using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MyBlog.Category.Repository;
using MyBlog.Category.Repository.Implements;
using MyBlog.Category.Repository.Interfaces;
using MyBlog.Category.Service.MappingProfiles;
using MyBlog.Shared.Autofac.Modules;
using MyBlog.Shared.RepositoryEF.DependencyInjection.Extensions;

namespace MyBlog.Category.Api.DependencyInjection.Extensions;

public static class HostingExtension
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddRouting(x => x.LowercaseUrls = true);
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
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

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        return app; 
    }
}
