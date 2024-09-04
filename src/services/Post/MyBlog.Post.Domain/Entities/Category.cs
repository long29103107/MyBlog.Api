using Contracts.Domain;

namespace MyBlog.Post.Domain.Entities;

public class Category : AuditEntity<int>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int? ParentCategoryId { get; private set; }
    public Category ParentCategory { get; private set; }
    
    private readonly List<Post> _posts = new List<Post>();
    public IReadOnlyCollection<Post> Posts => _posts.AsReadOnly();

    public Category() {}

    public Category(string name, string description, int? parentCategoryId)
    {
        SetName(name);
        SetDescription(description);
        SetParentCategoryId(parentCategoryId);
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be null or empty.");
        }
        Name = name;
    }

    public void SetDescription(string description)
    {
        Description = description;
    }

    public void SetParentCategoryId(int? parentCategoryId)
    {
        ParentCategoryId = parentCategoryId;
    }

    public void SetParentCategory(Category parentCategory)
    {
        ParentCategory = parentCategory;
    }

    public void AddPost(Post post)
    {
        if (post == null)
            throw new ArgumentNullException(nameof(post));

        if (!_posts.Contains(post))
        {
            _posts.Add(post);
            post.AddCategory(this);
        }
    }

    public void RemovePost(Post post)
    {
        if (post == null)
            throw new ArgumentNullException(nameof(post));

        if (_posts.Contains(post))
        {
            _posts.Remove(post);
            post.RemoveCategory(this);
        }
    }
}