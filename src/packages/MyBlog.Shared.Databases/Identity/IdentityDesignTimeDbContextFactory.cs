using MyBlog.Identity.Repository;

namespace MyBlog.Shared.Databases.Product;

public class IdentityDesignTimeDbContextFactory : DesignTimeDbContextFactory<MyIdentityDbContext>
{
    protected override string _dbConnStrKey { get; set; } = "Identity";
}