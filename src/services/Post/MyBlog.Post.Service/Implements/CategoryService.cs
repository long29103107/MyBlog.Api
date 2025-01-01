using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Infrastructures.Common;
using Microsoft.EntityFrameworkCore;
using MyBlog.Post.Domain.Exceptions;
using MyBlog.Post.Repository.Abstractions;
using MyBlog.Post.Service.Abstractions;
using static Shared.Dtos.Post.CategoryDtos;
using Entities = MyBlog.Post.Domain.Entities;
using FilteringAndSortingExpression.Extensions;
using Contracts.Domain.Exceptions.Abtractions;
using Contracts.Abstractions.Common;
using MyBlog.Post.Repository;
using MyBlog.Post.Domain.Entities;

namespace MyBlog.Post.Service.Implements;

public class CategoryService : BaseService<IRepositoryManager>, ICategoryService
{
    private readonly IValidatorFactory _validatorFactory;
    public CategoryService(IRepositoryManager repoManager, IMapper mapper, IValidatorFactory validatorFactory) : base(repoManager, mapper)
    {
        _validatorFactory = validatorFactory;
    }

    public async Task<CategoryResponse> CreateAsync(CategoryCreateRequest request)
    {
        var category = new Entities.Category(request.Name, request.Description, request.ParentCategoryId);

        _repoManager.Category.Add(category);
        await _repoManager.SaveAsync();

        return _mapper.Map<CategoryResponse>(category);
    }

    public async Task<CategoryResponse> GetAsync(int id)
    {
        var category = await _InternalGetCategoryAsync(id);
        
        var result = _mapper.Map<CategoryResponse>(category);

        return result;
    }

    public async Task<IEnumerable<CategoryResponse>> GetListAsync(CategoryListRequest request)
    {
        var result = await _repoManager.Category
            .FindAll()
            .Filter(request)
            .ProjectTo<CategoryResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return result;
    }

    public async Task<CategoryResponse> UpdateAsync(int id, CategoryUpdateRequest request)
    {
        var category = await _InternalGetCategoryAsync(id);

        category.SetName(request.Name);
        category.SetDescription(request.Description);
        category.SetParentCategoryId(request.ParentCategoryId);

        _repoManager.Category.Update(category);
        await _repoManager.SaveAsync();

        var result = _mapper.Map<CategoryResponse>(category);

        return result;
    }

    public async Task DeleteAsync(int id)
    {
        var category = await _InternalGetCategoryAsync(id);

        var existingChilrenCategory = await _repoManager.Category
            .AnyAsync(x => x.ParentCategoryId == id);

        if (existingChilrenCategory)
            throw new ErrorException("Cannot delete a category that is the parent category of another category!");

       var posts = category.Posts.ToList();

        posts.ForEach(x => x.RemoveCategory(category));

        _repoManager.Post.UpdateRange(posts);
        _repoManager.Category.Remove(category);
        await _repoManager.SaveAsync();
    }

    public async Task AddPostToCategoryAsync(int categoryId, int postId)
    {
        var category = await _InternalGetCategoryAsync(categoryId);

        var post = await _repoManager.Post
            .FirstOrDefaultAsync(x => x.Id == postId)
            ?? throw new PostException.NotFound(categoryId); 

        if (!category.Posts.Contains(post))
        {
            category.AddPost(post);
            _repoManager.Category.Update(category);
            await _repoManager.SaveAsync();
        }
    }

    public async Task RemovePostFromCategoryAsync(int categoryId, int postId)
    {
        var category = await _InternalGetCategoryAsync(categoryId);

        var post = await _repoManager.Post
               .FirstOrDefaultAsync(x => x.Id == postId)
               ?? throw new PostException.NotFound(categoryId);

        if (category.Posts.Contains(post))
        {
            category.RemovePost(post);
            _repoManager.Category.Update(category);
            await _repoManager.SaveAsync();
        }
    }

    private async Task<Entities.Category> _InternalGetCategoryAsync(int id)
        => await _repoManager.Category
            .FindByCondition(x => x.Id == id)
            .Include(c => c.Posts)
            .FirstOrDefaultAsync()
            ?? throw new CategoryException.NotFound(id);
}