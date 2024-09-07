using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlog.Post.Repository.Abstractions;
using MyBlog.Shared.Lib;
using MyBlog.Shared.Lib.Extensions;
using Entities = MyBlog.Post.Domain.Entities;
using FluentValidation;
using MyBlog.Post.Service.Abstractions;
using static Shared.Dtos.Post.PostDtos;
using Infrastructures.Common;
using Contracts.Abstractions.Shared;
using Infrastructures.DependencyInjection.Extensions;
using Contracts.Dtos;
using Microsoft.AspNetCore.Http;
using FilteringAndSortingExpression.Extensions;
using MyBlog.Post.Repository;
using Contracts.Abstractions.Common;

namespace MyBlog.Post.Service.Implements;

public class PostService : BaseService<IRepositoryManager, PostDbContext>, IPostService
{
    public PostService(IRepositoryManager repoManager, IMapper mapper, IValidatorFactory validatorFactory, IUnitOfWork<PostDbContext> unitOfWork) : base(repoManager, mapper, validatorFactory, unitOfWork)
    {
       
    }

    public async Task<ResponseListResult<PostListResponse>> GetListAsync(PostListRequest request)
    {
        var posts = await _repoManager.Post.FindAll()
            .Filter(request)
            .ToListAsync();

        var result = _mapper.Map<List<PostListResponse>>(posts);

        return ResponseListResult<PostListResponse>.Success(result);
    }

    public async Task<ResponseResult<PostResponse>> GetAsync(int id)
    {
        var post = await _InternalGetProductAsync(id);

        if(!post.IsSuccess)
        {
            return _GetFailedResult<PostResponse>(post.Errors, post.StatusCode);
        }

        var result = _mapper.Map<PostResponse>(post.Result);

        return ResponseResult<PostResponse>.Success(result);
    }


    public async Task<PagingListResponse<PostResponse>> GetPagedListAsync(PagingPostRequest request)
    {
        var dataset = _repoManager.Post.FindAll()
            .Select(x => new PostResponse(x.Id, x.Content))
            .Filter(request);

        var result = await dataset.GetMakeListAsync(request);

        return PagingListResponse<PostResponse>.Success(result);
    }

    public async Task<ResponseResult<PostResponse>> CreateAsync(PostCreateRequest request)
    {
        var model = _mapper.Map<Entities.Post>(request);

        var validatedResult = await _ValidateProductAsync(model);
        if(!validatedResult.IsSuccess)
        {
            return _GetFailedResult<PostResponse>(validatedResult.Errors, validatedResult.StatusCode);
        }

        _repoManager.Post.Add(model);

        var savedResult = await _SaveAsync();
        if (!savedResult.IsSuccess)
        {
            return _GetFailedResult<PostResponse>(savedResult.Errors, savedResult.StatusCode);
        }

        return ResponseResult<PostResponse>.Success(_mapper.Map<PostResponse>(model));
    }

    public async Task<ResponseResult<PostResponse>> UpdateAsync(int id, PostUpdateRequest request)
    {
        var model = await _InternalGetProductAsync(id);
        if (!model.IsSuccess)
        {
            return _GetFailedResult<PostResponse>(model.Errors, model.StatusCode);
        }

        _mapper.Map<PostUpdateRequest, Entities.Post>(request, model.Result!);

        var validatedResult = await _ValidateProductAsync(model.Result);
        if (!validatedResult.IsSuccess)
        {
            return _GetFailedResult<PostResponse>(validatedResult.Errors, validatedResult.StatusCode);
        }

        _repoManager.Post.Update(model.Result);

        var savedResult = await _SaveAsync();
        if (!savedResult.IsSuccess)
        {
            return _GetFailedResult<PostResponse>(savedResult.Errors, savedResult.StatusCode);
        }

        return ResponseResult<PostResponse>.Success(_mapper.Map<PostResponse>(model.Result));
    }

    public async Task<ResponseResult<PostResponse>> UpdatePartialAsync(int id, [FromBody] JsonPathRequest<PostUpdatePartialRequest> request)
    {
        var model = await _InternalGetProductAsync(id);
        if (!model.IsSuccess)
        {
            return _GetFailedResult<PostResponse>(model.Errors, model.StatusCode);
        }

        request.ApplyTo(model, _mapper);

        var validatedResult = await _ValidateProductAsync(model.Result);
        if (!validatedResult.IsSuccess)
        {
            return _GetFailedResult<PostResponse>(validatedResult.Errors, validatedResult.StatusCode);
        }
        _repoManager.Post.Update(model.Result);

        var savedResult = await _SaveAsync();
        if (!savedResult.IsSuccess)
        {
            return _GetFailedResult<PostResponse>(savedResult.Errors, savedResult.StatusCode);
        }

        return ResponseResult<PostResponse>.Success(_mapper.Map<PostResponse>(model));
    }

    public async Task<ResponseResult> DeleteAsync(int id)
    {
        var model = await _InternalGetProductAsync(id);
        if (!model.IsSuccess)
        {
            return _GetFailedResult<PostResponse>(model.Errors, model.StatusCode);
        }

        _repoManager.Post.Remove(model.Result);
        var savedResult = await _SaveAsync();
        if (!savedResult.IsSuccess)
        {
            return _GetFailedResult<PostResponse>(savedResult.Errors, savedResult.StatusCode);
        }

        return ResponseResult.Success();
    }

    private async Task<ResponseResult<Entities.Post>> _InternalGetProductAsync(int id)
    {
        var post = await _repoManager.Post
           .FindByCondition(x => x.Id == id)
           .FirstOrDefaultAsync();

        if (post is null)
        {
            return ResponseResult<Entities.Post>.Failure(new Error(ErrorCode.NotFound, $"The Post {id} is not found"), StatusCodes.Status404NotFound);
        }

        return ResponseResult<Entities.Post>.Success(post);
    }

    private async Task<ResponseResult> _ValidateProductAsync(Entities.Post model)
    {
        var validator = _validatorFactory.GetValidator<Entities.Post>();
        var result = await validator.ValidateAsync(model);

        if (!result.IsValid)
        {
            //var errors = result.Errors.Select(x => new ValidationError(x.PropertyName, x.ErrorMessage)).ToList();

            var errors = result.Errors.Select(x => new Error(x.PropertyName, x.ErrorMessage)).ToList();

            return ResponseResult<Entities.Post>.Failure(errors, StatusCodes.Status400BadRequest);
        }

        return ResponseResult.Success();
    }
}