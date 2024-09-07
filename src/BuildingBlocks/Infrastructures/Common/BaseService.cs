using AutoMapper;
using Contracts.Abstractions.Common;
using Contracts.Abstractions.Shared;
using Contracts.Dtos;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ILogger = Serilog.ILogger;

namespace Infrastructures.Common;

public abstract class BaseService<TRepoManager, TContext> : IBaseService<TRepoManager, TContext>
    where TContext : DbContext
{
    protected readonly TRepoManager _repoManager;
    protected readonly IMapper _mapper;
    protected readonly IValidatorFactory _validatorFactory;
    protected readonly IUnitOfWork<TContext> _unitOfWork;
    //protected ILogger _logger;

    protected BaseService(TRepoManager repoManager, IMapper mapper, IValidatorFactory validatorFactory, IUnitOfWork<TContext> unitOfWork)//, ILogger logger)
    {
        _repoManager = repoManager ?? throw new ArgumentNullException(nameof(_repoManager));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        _validatorFactory = validatorFactory ?? throw new ArgumentNullException(nameof(_validatorFactory));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(_unitOfWork));
        //_logger = logger;
    }

    protected async Task<ResponseResult> _SaveAsync()
    {
        try
        {
            await _unitOfWork.SaveAsync();
        }
        catch (Exception ex)
        {
            return ResponseResult.Failure(new Error(ErrorCode.ServerError, ex.Message), StatusCodes.Status500InternalServerError);
        }

        return ResponseResult.Success();
    }

    protected ResponseResult<T> _GetFailedResult<T>(List<Error> errors, int statusCode)
    {
        return ResponseResult<T>.Failure(errors, statusCode);
    }
}