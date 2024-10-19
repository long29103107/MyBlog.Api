using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Entities = MyBlog.Identity.Domain.Entities;

namespace MyBlog.Identity.Repository.Configurations;

public class OperationConfiguration : IEntityTypeConfiguration<Entities.Operation>
{
    public void Configure(EntityTypeBuilder<Entities.Operation> builder)
    {
        builder.HasIndex(c => c.Code)
            .IsUnique();
    }
}