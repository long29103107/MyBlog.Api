using MyBlog.Category.Repository;

namespace MyBlog.Shared.Databases.Category;

public class CategoryDesignTimeDbContextFactory : DesignTimeDbContextFactory<CategoryDbContext>
{
    protected override string _dbConnStrKey { get; set; } = "Category";
}