using Contracts.Dtos;
using MediatR;

namespace Contracts.Abstractions.Message;

public interface ICommand : IRequest<ResponseResult>
{
}

public interface ICommand<TResponse> : IRequest<ResponseResult<TResponse>>
{
}
