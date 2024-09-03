using Contracts.Domain;

namespace MyBlog.Post.Domain.Entities;

public class Comment : AuditEntity<int>
{
    public int PostId { get; set; }
    public string Content { get; set; }
    public Comment ParentComment { get; set; }
    public Post Post { get; set; }
    public List<Comment> Replies { get; set; } = new List<Comment>();
}