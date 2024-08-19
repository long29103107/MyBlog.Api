using Microsoft.EntityFrameworkCore;
using Entities = MyBlog.Category.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBlog.Contracts.Domains.ValueOf;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MyBlog.Category.Repository.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Entities.Category>
{
    public void Configure(EntityTypeBuilder<Entities.Category> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion<CategoryIdConversion>();
    }
}

public class CategoryIdConversion : ValueConverter<CategoryId, int>
{
    public CategoryIdConversion()
        : base(
            categoryId => categoryId.Value,
            value => CategoryId.From(value)
        )
    { }
}