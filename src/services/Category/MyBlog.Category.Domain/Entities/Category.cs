using MyBlog.Contracts.Domains;
using MyBlog.Contracts.Domains.ValueOf;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlog.Category.Domain.Entities;

public class Category : EntityAuditBase<CategoryId>
{
    [Column(TypeName = "varchar(50)")]
    public string Name { get; set; }
    [Column(TypeName = "varchar(50)")]
    public string SlugName { get; set; }
}
