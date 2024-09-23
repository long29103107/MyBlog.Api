﻿using Contracts.Dtos;
using MediatR;

namespace Contracts.Abstractions.Message;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, ResponseResult>
    where TCommand : ICommand
{ }

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, ResponseResult>
    where TCommand : ICommand<TResponse>
{ }
