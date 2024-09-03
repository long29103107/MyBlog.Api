using Contracts.Domain;

namespace MyBlog.Post.Domain.Entities;

public class PostImage : AuditEntity<int>
{
    public int PostId { get; set; }
    public string URL { get; set; }
    public string Caption { get; set; }
    public Post Post { get; set; }
}