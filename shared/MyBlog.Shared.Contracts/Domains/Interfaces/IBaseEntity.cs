namespace MyBlog.Shared.Contracts.Domains.Interfaces;

public interface IBaseEntity<T>
{
    public T Id { get; set; }
}
