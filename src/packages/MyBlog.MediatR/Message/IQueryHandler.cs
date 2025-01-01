using Contracts.Dtos;
using MediatR;

namespace MyBlog.MediatR.Message;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, ResponseResult<TResponse>>
    where TQuery : IQuery<TResponse>
{ }
