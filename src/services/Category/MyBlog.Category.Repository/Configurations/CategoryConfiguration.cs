using Microsoft.EntityFrameworkCore;
using Entities = MyBlog.Category.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyBlog.Category.Repository.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Entities.Category>
{
    public void Configure(EntityTypeBuilder<Entities.Category> builder)
    {
        builder.HasIndex(x => x.SlugName).IsUnique();
    }
}