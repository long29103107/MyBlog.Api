using Contracts.Domain;

namespace MyBlog.Post.Domain.Entities;

public class Post : AggregateRoot<int>
{
    public string Title { get; private set; }
    public string Content { get; private set; }

    private readonly List<PostImage> _images = new List<PostImage>();
    private readonly List<PostMetadata> _metadata = new List<PostMetadata>();
    private readonly List<Tag> _tags = new List<Tag>();
    private readonly List<Category> _categories = new List<Category>();
   
    public IReadOnlyCollection<PostImage> Images => _images.AsReadOnly();
    public IReadOnlyCollection<PostMetadata> Metadata => _metadata.AsReadOnly();
    public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();
    public IReadOnlyCollection<Category> Categories => _categories.AsReadOnly();

    public Post() {}

    public Post(string title, string content, Category category)
    {
        SetTitle(title);
        SetContent(content);
    }

    public void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title cannot be null or empty.");
        }
        Title = title;
    }

    public void SetContent(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            throw new ArgumentException("Content cannot be null or empty.");
        }
        Content = content;
    }

    public void AddCategory(Category category)
    {
        if (category == null)
            throw new ArgumentNullException(nameof(category));

        if (!_categories.Contains(category))
        {
            _categories.Add(category);
            category.AddPost(this);
        }
    }

    public void RemoveCategory(Category category)
    {
        if (category == null)
            throw new ArgumentNullException(nameof(category));

        if (_categories.Contains(category))
        {
            _categories.Remove(category);
            category.RemovePost(this);
        }
    }

    public void AddImage(string url, string description)
    {
        var image = new PostImage(url, description);
        _images.Add(image);
    }

    public void AddMetadata(string key, string value)
    {
        var metadata = new PostMetadata(key, value);
        _metadata.Add(metadata);
    }
    public void AddTag(string tagName)
    {
        var tag = new Tag(tagName);
        _tags.Add(tag);
    }

    public void RemoveTag(string tagName)
    {
        var tag = _tags.FirstOrDefault(t => t.Name == tagName);
        if (tag != null)
        {
            _tags.Remove(tag);
        }
    }
}
