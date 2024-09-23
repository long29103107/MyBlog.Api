using Contracts.Dtos;
using MediatR;

namespace DistributedSystem.Contract.Abstractions.Message;

public interface IQuery<TResponse> : IRequest<ResponseResult<TResponse>>
{ }
