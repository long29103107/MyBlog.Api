using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyBlog.Identity.Repository;
using MyBlog.Post.Repository;

namespace MyBlog.Shared.Databases.Product;

public class IdentityDesignTimeDbContextFactory : DesignTimeDbContextFactory<MyIdentityDbContext>
{
    protected override string _dbConnStrKey { get; set; } = "Identity";
}