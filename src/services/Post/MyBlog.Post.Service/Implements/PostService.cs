using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlog.Post.Repository.Interfaces;
using MyBlog.Shared.Lib;
using MyBlog.Shared.ServiceBase.Implements;
using MyBlog.Shared.Lib.Extensions;
using Entities = MyBlog.Post.Domain.Entities;
using FluentValidation;
using MyBlog.Post.Service.Interfaces;
using MyBlog.Contracts;
using MyBlog.Shared.Dtos.Post;
using MyBlog.Post.Domain.Exceptions;

namespace MyBlog.Post.Service.Implements;

public class PostService : BaseService<IRepositoryManager>, IPostService
{
    private readonly IValidatorFactory _validatorFactory;

    public PostService(IRepositoryManager repoManager, IMapper mapper, IValidatorFactory validatorFactory) : base(repoManager, mapper)
    {
        _validatorFactory = validatorFactory;
    }

    public async Task<List<PostListResponse>> GetListAsync()
    {
        var posts = await _repoManager.Post.FindAll()
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

        _mapper.Map<PostUpdateRequest, Entities.Post>(request, model!);

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

    public async Task SeedDataAsync()
    {
        if (!await _repoManager.Post.AnyAsync())
        {
            var index = 0;
            var products = new List<Entities.Post>();

            while (index < 1000)
            {
                var name = "Product " + (index + 1);
                products.Add(new Entities.Post()
                {
                    Title = name,
                    Description = name
                });
                index++;
            }

            _repoManager.Post.AddRange(products);
            await _repoManager.SaveAsync();
        }
    }

    private async Task<Entities.Post> _InternalGetProductAsync(int id)
        => await _repoManager.Post
           .FindByCondition(x => x.Id == id)
           .FirstOrDefaultAsync()
             ?? throw new PostException.NotFound(id);

    private async Task _ValidateProductAsync(Entities.Post model)
    {
        var validator = _validatorFactory.GetValidator<Entities.Post>();
        var result = await validator.ValidateAsync(model);

        if (!result.IsValid)
        {
            var errors = result.Errors.Select(x => new ValidationError(x.PropertyName, x.ErrorMessage)).ToList();

            throw new Contracts.ValidationException(errors);
        }
    }
}