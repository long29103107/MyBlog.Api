using Contracts.Abstractions.Common;
using MyBlog.Post.Repository.Abstractions;
using static Shared.Dtos.Category.CategoryDtos;

namespace MyBlog.Post.Service.Abstractions;

public interface ICategoryService : IBaseService<IRepositoryManager>
{
    Task<CategoryResponse> CreateAsync(CategoryCreateRequest request);
    Task<CategoryResponse> GetAsync(int id);
    Task<IEnumerable<CategoryResponse>> GetListAsync(CategoryListRequest request);
    Task<CategoryResponse> UpdateAsync(int id, CategoryUpdateRequest category);
    Task DeleteAsync(int id);
    Task AddPostToCategoryAsync(int categoryId, int postId);
    Task RemovePostFromCategoryAsync(int categoryId, int postId);
}