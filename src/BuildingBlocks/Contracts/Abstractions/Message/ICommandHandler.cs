using Contracts.Abstractions.Shared;
using MediatR;

namespace Contracts.Abstractions.Message;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Response>
    where TCommand : ICommand
{ }

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Response>
    where TCommand : ICommand<TResponse>
{ }
