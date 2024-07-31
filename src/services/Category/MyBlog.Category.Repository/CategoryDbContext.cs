using Microsoft.EntityFrameworkCore;
using MyBlog.Category.Repository.Configurations;
using Entities = MyBlog.Category.Domain.Entities;

namespace MyBlog.Category.Repository;

public class CategoryDbContext : DbContext
{
    public CategoryDbContext(DbContextOptions<CategoryDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Entities.Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}