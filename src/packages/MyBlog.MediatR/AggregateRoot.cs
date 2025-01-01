using Contracts.Domain;
using MyBlog.MediatR.Message;

namespace MyBlog.MediatR;

public abstract class AggregateRoot<T> :  AuditEntity<T>
{
    private readonly List<IDomainEvent> _domainEvents = new();

    public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => _domainEvents.ToList();

    public void ClearDomainEvents() => _domainEvents.Clear();

    public void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}