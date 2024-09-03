using Contracts.Abstractions.Shared;
using MediatR;

namespace DistributedSystem.Contract.Abstractions.Message;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Response<TResponse>>
    where TQuery : IQuery<TResponse>
{ }
