using Contracts.Domain;

namespace MyBlog.Post.Domain.Entities;

public class PostMetadata : AuditEntity<int>
{
    public string Key { get; private set; }
    public string Value { get; private set; }
    public PostMetadata() { }
    public PostMetadata(string key, string value)
    {
        Key = key;
        Value = value;
    }
}