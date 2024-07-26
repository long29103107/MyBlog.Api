using MyBlog.Contracts.Domains.Interfaces;

namespace MyBlog.Contracts.Domains;

public abstract class EntityAuditBase<T> : EntityBase<T>, ITracking
{
    public DateTimeOffset CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public string UpdatedBy { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public string DeletedBy { get; set; }
}