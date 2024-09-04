using Contracts.Abstractions.Common;
using MyBlog.Post.Repository.Abstractions;
using static Shared.Dtos.Category.CategoryDtos;

namespace MyBlog.Post.Service.Abstractions;

public interface ICategoryService : IBaseService<IRepositoryManager>
{
    Task<CategoryResponse> CreateAsync(CategoryCreateRequest request);
    Task<CategoryResponse> GetDetailAsync(int id);
    Task<IEnumerable<CategoryResponse>> GetCategoriesAsync();
    Task<bool> UpdateAsync(CategoryUpdateRequest category);
    Task<bool> DeleteAsync(int id);
    Task<bool> AddPostToCategoryAsync(int categoryId, int postId);
    Task<bool> RemovePostFromCategoryAsync(int categoryId, int postId);
}