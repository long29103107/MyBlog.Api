using MyBlog.Contracts.Domains;

namespace MyBlog.Category.Domain.Entities;
public class Category : EntityAuditBase<int>
{
    public string Name { get; set; }
    public string SlugName { get; set; }
}
