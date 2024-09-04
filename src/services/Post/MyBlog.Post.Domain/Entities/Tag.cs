using Contracts.Domain;
using static System.Net.Mime.MediaTypeNames;

namespace MyBlog.Post.Domain.Entities;

public class Tag : AuditEntity<int>
{
    public string Name { get; private set; }
    public Tag() { }
    public Tag(string name)
    {
        Name = name;
    }

    private readonly List<Post> _posts = new List<Post>();
    public IReadOnlyCollection<Post> Posts => _posts.AsReadOnly();
}