using MyBlog.Contracts.Domains;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlog.Post.Domain.Entities;

public class Post : EntityAuditBase<int>
{
    [Column(TypeName = "varchar")]
    [MaxLength]
    public string? Title { get; set; }
    [Column(TypeName = "varchar(255)")]
    public string? Description { get; set; }
}
