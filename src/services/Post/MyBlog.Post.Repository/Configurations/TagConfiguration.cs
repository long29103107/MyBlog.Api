using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBlog.Post.Domain.Entities;
using Entities = MyBlog.Post.Domain.Entities;

namespace MyBlog.Post.Repository.Configurations;

public class TagConfiguration : IEntityTypeConfiguration<Entities.Tag>
{
    public void Configure(EntityTypeBuilder<Entities.Tag> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
           .IsRequired()
           .HasMaxLength(100);
    }
}