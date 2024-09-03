using Contracts.Domain;
using MyBlog.Post.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace MyBlog.Post.Domain.Entities;

public class Post : AggregateRoot<int>
{
    public string Title { get; set; }
    public string Content { get; set; }
    public PostStatus Status { get; set; }
    public List<Tag> Tags { get; set; } = new List<Tag>();
    public List<Category> Categories { get; set; } = new List<Category>();
    public List<Comment> Comments { get; set; } = new List<Comment>();
    public List<PostImage> Images { get; set; } = new List<PostImage>();
    public List<PostMetadata> Metadata { get; set; } = new List<PostMetadata>();
}
