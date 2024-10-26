using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MyBlog.Shared.Databases;

public class DesignTimeDbContextFactory<T> : IDesignTimeDbContextFactory<T> where T : DbContext
{
    protected virtual string _dbConnStrKey { get; set; } = "DefaultConnection";

    public virtual T CreateDbContext(string[] args)
    {
        Console.WriteLine(Directory.GetCurrentDirectory() + " " + typeof(T).FullName + " " + _dbConnStrKey);
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        var builder = new DbContextOptionsBuilder<T>();
        var connectionString = configuration.GetSection($"ConnectionStrings:{_dbConnStrKey}").Value;
        Console.WriteLine(connectionString);

        switch(_dbConnStrKey)
        {
            case "Post":
                builder.UseNpgsql(connectionString, b => b.MigrationsAssembly(SharedDatabaseReference.AssemblyName));
                break;
            default:
                builder.UseSqlServer(connectionString, b => b.MigrationsAssembly(SharedDatabaseReference.AssemblyName));
                break;
        }

        var dbContext = (T)Activator.CreateInstance(typeof(T), builder.Options);
        return dbContext;
    }
}