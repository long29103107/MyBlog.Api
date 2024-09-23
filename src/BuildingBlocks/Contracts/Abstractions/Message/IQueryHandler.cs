using Contracts.Dtos;
using MediatR;

namespace DistributedSystem.Contract.Abstractions.Message;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, ResponseResult<TResponse>>
    where TQuery : IQuery<TResponse>
{ }
