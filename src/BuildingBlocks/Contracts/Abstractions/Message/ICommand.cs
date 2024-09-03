using Contracts.Abstractions.Shared;
using MediatR;

namespace Contracts.Abstractions.Message;

public interface ICommand : IRequest<Response>
{
}

public interface ICommand<TResponse> : IRequest<Response<TResponse>>
{
}
