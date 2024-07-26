using MyBlog.Contracts.Domains.Interfaces;

namespace MyBlog.Contracts.Domains;

public abstract class EntityBase<TKey> : IEntityBase<TKey>
{
    public TKey Id { get; set; }
}