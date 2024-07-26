using Microsoft.EntityFrameworkCore;
using Entities = MyBlog.Category.Domain.Entities;

namespace MyBlog.Category.Repository;

public class CategoryDbContext : DbContext
{
    public CategoryDbContext(DbContextOptions<CategoryDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Entities.Category> Categories { get; set; }
}