using Contracts.Dtos;
using MediatR;

namespace MyBlog.MediatR.Message;

public interface ICommand : IRequest<ResponseResult>
{
}

public interface ICommand<TResponse> : IRequest<ResponseResult<TResponse>>
{
}
