using MyBlog.Category.Repository.Implements;
using MyBlog.Category.Repository.Interfaces;
using MyBlog.Contracts.Domains.ValueOf;
using MyBlog.Shared.Databases.Category;
using MyBlog.Shared.Dtos.Category;
using MyBlog.Shared.Lib;
using MyBlog.Shared.ServiceBase.Abstractions;

namespace MyBlog.Category.Service.Interfaces;

public interface ICategoryService : IBaseService<RepositoryManager>
{
    Task<List<ListCategoryResponse>> GetListAsync();
    Task<CategoryResponse> GetAsync(CategoryId id);
    Task<CategoryResponse> CreateAsync(CreateCategoryRequest request);
    Task<CategoryResponse> UpdateAsync(CategoryId id, UpdateCategoryRequest request);
    Task<CategoryResponse> UpdatePartialAsync(CategoryId id, JsonPathRequest<UpdatePartialCategoryRequest> request);
    Task SeedDataAsync();
}

