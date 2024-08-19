using Microsoft.EntityFrameworkCore;
using Entities = MyBlog.Category.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBlog.Contracts.Domains.ValueOf;

namespace MyBlog.Category.Repository.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Entities.Category>
{
    public void Configure(EntityTypeBuilder<Entities.Category> builder)
    {
        builder.HasKey(x => x.Id);
        //builder.Property(x => x.Id).HasConversion(x => x, x => CategoryId.From(x.Value));
        builder.HasIndex(x => x.SlugName).IsUnique();
    }
}