using Contracts.Domain;

namespace MyBlog.Post.Domain.Entities;

public class Tag : AuditEntity<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Post> Posts { get; set; } = new List<Post>();
}