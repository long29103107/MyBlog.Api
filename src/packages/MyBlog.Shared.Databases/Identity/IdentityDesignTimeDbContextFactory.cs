using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyBlog.Idenity.Api.Authentication;
using MyBlog.Post.Repository;

namespace MyBlog.Shared.Databases.Product;

public class IdentityDesignTimeDbContextFactory : DesignTimeDbContextFactory<MyIdentityDbContext>
{
    protected override string _dbConnStrKey { get; set; } = "Identity";
}