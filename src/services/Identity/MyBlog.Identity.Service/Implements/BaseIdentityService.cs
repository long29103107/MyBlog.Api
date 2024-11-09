using AutoMapper;
using Contracts.Abstractions.Common;
using Contracts.Abstractions.Shared;
using Contracts.Dtos;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MyBlog.Identity.Repository.Abstractions;
using MyBlog.Identity.Service.Abstractions;
using Serilog;

namespace MyBlog.Identity.Service.Implements;

public abstract class BaseIdentityService : IBaseIdentityService
{
    protected readonly IRepositoryManager _repoManager;
    protected readonly IMapper _mapper;
    protected readonly IValidatorFactory _validatorFactory;
    protected readonly ILogger _logger;

    protected BaseIdentityService(IRepositoryManager repoManager, IMapper mapper, IValidatorFactory validatorFactory, ILogger logger)
    {
        _repoManager = repoManager ?? throw new ArgumentNullException(nameof(_repoManager));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        _validatorFactory = validatorFactory ?? throw new ArgumentNullException(nameof(_validatorFactory));
        _logger = logger;
    }

    protected async Task _SaveAsync()
    {
        try
        {
            await _repoManager.SaveAsync();
        }
        catch (Exception ex)
        {
            //TODO: Add log
            throw;
        }
    }

    //protected async Task<ResponseResult> _SaveAsync()
    //{
    //    try
    //    {
    //        await _unitOfWork.SaveAsync();
    //    }
    //    catch (Exception ex)
    //    {
    //        return ResponseResult.Failure(new Error(ErrorCode.ServerError, ex.Message), StatusCodes.Status500InternalServerError);
    //    }

    //    return ResponseResult.Success();
    //}

    protected ResponseResult<T> _GetFailedResult<T>(List<Error> errors, int statusCode)
    {
        return ResponseResult<T>.Failure(errors, statusCode);
    }
}