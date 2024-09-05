using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Infrastructures.Common;
using Microsoft.EntityFrameworkCore;
using MyBlog.Post.Domain.Exceptions;
using MyBlog.Post.Repository;
using MyBlog.Post.Repository.Abstractions;
using MyBlog.Post.Service.Abstractions;
using static Shared.Dtos.Category.CategoryDtos;
using static Shared.Dtos.Post.PostDtos;
using Entities = MyBlog.Post.Domain.Entities;

namespace MyBlog.Post.Service.Implements;

public class CategoryService : BaseService<IRepositoryManager>, ICategoryService
{
    public CategoryService(IRepositoryManager repoManager, IMapper mapper, IValidatorFactory validatorFactory) : base(repoManager, mapper)
    {
       
    }

    public async Task<CategoryResponse> CreateAsync(CategoryCreateRequest request)
    {
        var category = new Entities.Category(request.Name, request.Description, request.ParentCategoryId);

        _repoManager.Category.Add(category);
        await _repoManager.SaveAsync();

        return _mapper.Map<CategoryResponse>(category);
    }

    public async Task<CategoryResponse> GetDetailAsync(int id)
    {
        var category = await _InternalGetCategory(id)
            .ProjectTo<CategoryResponse>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync()
            ?? throw new CategoryException.NotFound(id);

        return category;
    }

    public async Task<IEnumerable<CategoryResponse>> GetCategoriesAsync()
    {
        var categories = await _repoManager.Category
            .FindAll()
            .ProjectTo<CategoryResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return categories;
    }

    public async Task<CategoryResponse> UpdateAsync(int id, CategoryUpdateRequest request)
    {
        var category = await _repoManager.Category
            .FirstOrDefaultAsync(x => x.Id == id)
             ?? throw new CategoryException.NotFound(id);

        category.SetName(request.Name);
        category.SetDescription(request.Description);
        category.SetParentCategoryId(request.ParentCategoryId);

        _repoManager.Category.Update(category);
        await _repoManager.SaveAsync();

        return _mapper.Map<CategoryResponse>(category);
    }

    public async Task DeleteAsync(int id)
    {
        var category = await _repoManager.Category
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new CategoryException.NotFound(id);

        _repoManager.Category.Remove(category);
        await _repoManager.SaveAsync();
    }

    public async Task AddPostToCategoryAsync(int categoryId, int postId)
    {
        var category = await _InternalGetCategory(categoryId)
            .FirstOrDefaultAsync()
             ?? throw new CategoryException.NotFound(categoryId);

        var post = await _repoManager.Post
            .FirstOrDefaultAsync(x => x.Id == postId)
            ?? throw new PostException.NotFound(categoryId); 

        if (!category.Posts.Contains(post))
        {
            category.AddPost(post);
            await _repoManager.SaveAsync();
        }
    }

    public async Task RemovePostFromCategoryAsync(int categoryId, int postId)
    {
        var category = await _InternalGetCategory(categoryId)
            .FirstOrDefaultAsync()
             ?? throw new CategoryException.NotFound(categoryId);

        var post = await _repoManager.Post
               .FirstOrDefaultAsync(x => x.Id == postId)
               ?? throw new PostException.NotFound(categoryId);

        if (category.Posts.Contains(post))
        {
            category.RemovePost(post);
            await _repoManager.SaveAsync();
        }
    }

    private IQueryable<Entities.Category> _InternalGetCategory(int id)
        => _repoManager.Category
           .FindByCondition(x => x.Id == id)
           .Include(c => c.Posts);
}