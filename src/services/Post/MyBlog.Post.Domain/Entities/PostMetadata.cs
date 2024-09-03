using Contracts.Domain;

namespace MyBlog.Post.Domain.Entities;

public class PostMetadata : AuditEntity<int>
{
    public int PostId { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
    public Post Post { get; set; }
}