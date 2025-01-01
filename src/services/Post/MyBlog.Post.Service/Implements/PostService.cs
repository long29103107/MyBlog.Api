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
using FilteringAndSortingExpression.Extensions;
using Contracts.Domain.Exceptions;
using Exceptions = Contracts.Domain.Exceptions;
using MyBlog.Post.Domain.Exceptions;
using MyBlog.Post.Repository;
using Contracts.Abstractions.Common;
using System.Data;
using MyBlog.FluentValidation.Exceptions;
using ValidationException = MyBlog.FluentValidation.Exceptions.ValidationException;
namespace MyBlog.Post.Service.Implements;

public class PostService : BaseService<IRepositoryManager>, IPostService
{
    private readonly IValidatorFactory _validatorFactory;

    public PostService(
        IRepositoryManager repoManager
        , IMapper mapper
        , IValidatorFactory validatorFactory
        ) 
        : base(repoManager, mapper)
    {
        _validatorFactory = validatorFactory;
    }

    public async Task<List<PostListResponse>> GetListAsync(PostListRequest request)
    {
        var posts = await _repoManager.Post.FindAll()
            .Filter(request)
            .ToListAsync();

        var result = _mapper.Map<List<PostListResponse>>(posts);

        return result;
    }

    public async Task<PostResponse> GetAsync(int id)
    {
        var post = await _InternalGetProductAsync(id);

        var result = _mapper.Map<PostResponse>(post);

        return result;
    }

    public async Task<List<PostListResponse>> GetPagedListAsync(PagingPostRequest request)
    {
        var dataSet = _repoManager.Post.FindAll()
            .Select(x => new PostListResponse
            {
                Id = x.Id,
                Content = x.Content
            })
            .Filter(request);

        var result = await request.MakeListAsync(dataSet);

        return result;
    }

    public async Task<PostResponse> CreateAsync(PostCreateRequest request)
    {
        var model = _mapper.Map<Entities.Post>(request);

        await _ValidateProductAsync(model);

        _repoManager.Post.Add(model);

        await _repoManager.SaveAsync();

        return _mapper.Map<PostResponse>(model);
    }

    public async Task<PostResponse> UpdateAsync(int id, PostUpdateRequest request)
    {
        var model = await _InternalGetProductAsync(id);
        
        _mapper.Map<PostUpdateRequest, Entities.Post>(request, model);

        await _ValidateProductAsync(model);

        _repoManager.Post.Update(model);

        await _repoManager.SaveAsync();

        return _mapper.Map<PostResponse>(model);
    }

    public async Task<PostResponse> UpdatePartialAsync(int id, [FromBody] JsonPathRequest<PostUpdatePartialRequest> request)
    {
        var model = await _InternalGetProductAsync(id);

        request.ApplyTo(model, _mapper);

        await _ValidateProductAsync(model);

        _repoManager.Post.Update(model);

        await _repoManager.SaveAsync();

        return _mapper.Map<PostResponse>(model);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var model = await _InternalGetProductAsync(id);

        _repoManager.Post.Remove(model);

        await _repoManager.SaveAsync();

        return true;
    }

    private async Task<Entities.Post> _InternalGetProductAsync(int id)
    {
        var result = await _repoManager.Post
           .FindByCondition(x => x.Id == id)
           .FirstOrDefaultAsync()
           ?? throw new PostException.NotFound(id);

        return result;
    }

    private async Task _ValidateProductAsync(Entities.Post model)
    {
        var validator = _validatorFactory.GetValidator<Entities.Post>();
        var result = await validator.ValidateAsync(model);

        if (!result.IsValid)
        {
            var errors = result.Errors.Select(x => new ValidationError(x.PropertyName, x.ErrorMessage)).ToList();

            throw new ValidationException(errors);
        }
    }
}