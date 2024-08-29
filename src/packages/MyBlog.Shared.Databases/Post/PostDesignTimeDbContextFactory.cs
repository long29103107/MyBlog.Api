using MyBlog.Post.Repository;

namespace MyBlog.Shared.Databases.Product;

public class PostDesignTimeDbContextFactory : DesignTimeDbContextFactory<PostDbContext>
{
    protected override string _dbConnStrKey { get; set; } = "Post";
}