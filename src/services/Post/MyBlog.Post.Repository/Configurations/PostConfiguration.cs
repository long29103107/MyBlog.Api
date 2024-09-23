using Microsoft.EntityFrameworkCore;
using Entities = MyBlog.Post.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace MyBlog.Post.Repository.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Entities.Post>
{
    public void Configure(EntityTypeBuilder<Entities.Post> builder)
    {
        builder.HasMany(p => p.Tags)
            .WithMany(t => t.Posts)
            .UsingEntity<Dictionary<string, object>>(
                "PostTag",
                j => j.HasOne<Entities.Tag>().WithMany().HasForeignKey("TagId"),
                j => j.HasOne<Entities.Post>().WithMany().HasForeignKey("PostId"),
                j =>
                {
                    j.HasKey("PostId", "TagId");
                    j.ToTable("PostTags"); 
                });

        builder.HasMany(p => p.Categories)
         .WithMany(c => c.Posts)
         .UsingEntity<Dictionary<string, object>>(
             "PostCategory", 
             j => j.HasOne<Entities.Category>().WithMany().HasForeignKey("CategoryId").HasConstraintName("FK_PostCategory_Category_CategoryId"),
             j => j.HasOne<Entities.Post>().WithMany().HasForeignKey("PostId").HasConstraintName("FK_PostCategory_Post_PostId"),
             j =>
             {
                 j.HasKey("PostId", "CategoryId"); 
                 j.ToTable("PostCategories");
             });

        builder.HasMany(p => p.Images)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Metadata)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}