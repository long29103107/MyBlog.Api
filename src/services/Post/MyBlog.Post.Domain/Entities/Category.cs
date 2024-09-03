using Contracts.Domain;

namespace MyBlog.Post.Domain.Entities;

public class Category : AuditEntity<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Category ParentCategory { get; set; }
    public List<Post> Posts { get; set; } = new List<Post>();
}