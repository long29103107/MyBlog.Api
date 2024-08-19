using MyBlog.Contracts.Domains.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlog.Contracts.Domains;

public abstract class EntityAuditBase<T> : EntityBase<T>, ITracking//, ISofeDeleteTracking
{
    public DateTimeOffset CreatedAt { get; set; }
    [Column(TypeName = "varchar(50)")]
    public string CreatedBy { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    [Column(TypeName = "varchar(50)")]
    public string? UpdatedBy { get; set; }
    //public DateTimeOffset? DeletedAt { get; set; }
    //[Column(TypeName = "varchar(50)")]
    //public string? DeletedBy { get; set; }
}