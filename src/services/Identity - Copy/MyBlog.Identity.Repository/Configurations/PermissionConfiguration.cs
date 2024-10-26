//using MyBlog.Identity.Domain.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace MyBlog.Identity.Repository.Configurations;

//public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
//{
//    public void Configure(EntityTypeBuilder<Permission> builder)
//    {
//        builder.HasIndex(c => c.Code)
//           .IsUnique();
//    }
//}