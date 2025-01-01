namespace MyBlog.MediatR.Message;

public abstract class DomainEvent : IDomainEvent
{
    public Guid Id { get; init; }
}