using Microsoft.EntityFrameworkCore;
using Entities = MyBlog.Post.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace MyBlog.Post.Repository.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Entities.Category>
{
    public void Configure(EntityTypeBuilder<Entities.Category> builder)
    {
        builder.HasOne(c => c.ParentCategory)
            .WithMany()
            .HasForeignKey(c => c.ParentCategoryId);
    }
}