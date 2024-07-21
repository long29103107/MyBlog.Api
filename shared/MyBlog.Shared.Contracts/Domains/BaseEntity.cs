namespace MyBlog.Shared.Contracts.Domains.Interfaces;

public class BaseEntity<T> : IBaseEntity<T> where T : class
{
    public T Id { get; set; }
}