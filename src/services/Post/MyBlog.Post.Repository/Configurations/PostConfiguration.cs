using Microsoft.EntityFrameworkCore;
using Entities = MyBlog.Post.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBlog.Contracts.Domains.ValueOf;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MyBlog.Post.Repository.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Entities.Post>
{
    public void Configure(EntityTypeBuilder<Entities.Post> builder)
    {
        //builder.HasKey(x => x.Id);
        //builder.Property(x => x.Id).HasConversion<CategoryIdConversion>();
    }
}

//public class CategoryIdConversion : ValueConverter<CategoryId, int>
//{
//    public CategoryIdConversion()
//        : base(
//            categoryId => categoryId.Value,
//            value => CategoryId.From(value)
//        )
//    { }
//}