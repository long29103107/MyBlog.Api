using AutoMapper;
using FluentValidation;
using Infrastructures.Common;
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
        var category = await _context.Categories
                                     .Include(c => c.Posts)
                                     .FirstOrDefaultAsync(c => c.Id == id);
        if (category == null) return null;

        return new CategoryResponse
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            ParentCategoryId = category.ParentCategoryId,
            Posts = category.Posts.Select(p => new PostResponse
            {
                Id = p.Id,
                Title = p.Title
                // Other properties as needed
            }).ToList()
        };
    }

    public async Task<IEnumerable<CategoryResponse>> GetCategoriesAsync()
    {
        var categories = await _context.Categories.ToListAsync();

        return categories.Select(c => new CategoryResponse
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            ParentCategoryId = c.ParentCategoryId
        });
    }

    public async Task<bool> UpdateAsync(CategoryUpdateRequest request)
    {
        var category = await _context.Categories.FindAsync(request.Id);
        if (category == null) return false;

        category.Name = request.Name;
        category.Description = request.Description;
        category.ParentCategoryId = request.ParentCategoryId;

        _context.Categories.Update(category);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return false;

        _context.Categories.Remove(category);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> AddPostToCategoryAsync(int categoryId, int postId)
    {
        var category = await _context.Categories
                                     .Include(c => c.Posts)
                                     .FirstOrDefaultAsync(c => c.Id == categoryId);
        if (category == null) return false;

        var post = await _context.Posts.FindAsync(postId);
        if (post == null) return false;

        if (!category.Posts.Contains(post))
        {
            category.Posts.Add(post);
            return await _context.SaveChangesAsync() > 0;
        }

        return true;
    }

    public async Task<bool> RemovePostFromCategoryAsync(int categoryId, int postId)
    {
        var category = await _context.Categories
                                     .Include(c => c.Posts)
                                     .FirstOrDefaultAsync(c => c.Id == categoryId);
        if (category == null) return false;

        var post = category.Posts.FirstOrDefault(p => p.Id == postId);
        if (post == null) return false;

        category.Posts.Remove(post);
        return await _context.SaveChangesAsync() > 0;
    }
}