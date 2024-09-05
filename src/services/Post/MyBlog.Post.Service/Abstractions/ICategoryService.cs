using Contracts.Abstractions.Common;
using MyBlog.Post.Repository.Abstractions;
using static Shared.Dtos.Category.CategoryDtos;

namespace MyBlog.Post.Service.Abstractions;

public interface ICategoryService : IBaseService<IRepositoryManager>
{
    Task<CategoryResponse> CreateAsync(CategoryCreateRequest request);
    Task<CategoryResponse> GetDetailAsync(int id);
    Task<IEnumerable<CategoryResponse>> GetCategoriesAsync();
    Task<CategoryResponse> UpdateAsync(int id, CategoryUpdateRequest category);
    Task DeleteAsync(int id);
    Task AddPostToCategoryAsync(int categoryId, int postId);
    Task RemovePostFromCategoryAsync(int categoryId, int postId);
}