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

    public void SetName(string name)
    {
        Name = name;
    }
    public void AddPost(Post post)
    {
        _posts.Add(post);
    }
    public void RemovePost(Post post)
    {
        _posts.Remove(post);
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Tag other = (Tag)obj;
        return other.Id == Id;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }
}