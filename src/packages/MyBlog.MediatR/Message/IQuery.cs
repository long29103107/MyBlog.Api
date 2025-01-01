using Contracts.Dtos;
using MediatR;

namespace MyBlog.MediatR.Message;

public interface IQuery<TResponse> : IRequest<ResponseResult<TResponse>>
{ }
