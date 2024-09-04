using Contracts.Domain;

namespace MyBlog.Post.Domain.Entities;

public class PostImage : AuditEntity<int>
{
    public string Url { get; private set; }
    public string Description { get; private set; }
    public PostImage() { }
    public PostImage(string url, string description)
    {
        Url = url;
        Description = description;
    }
}