using MediatR;

namespace MyBlog.MediatR.Message;

public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
}
