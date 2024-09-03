using Contracts.Abstractions.Common;
using MyBlog.Post.Repository.Implements;
using MyBlog.Shared.Lib;
using static Shared.Dtos.Post.PostDtos;

namespace MyBlog.Post.Service.Abstractions;

public interface IPostService : IBaseService<RepositoryManager>
{
    Task<List<PostListResponse>> GetListAsync();
    Task<PostResponse> GetAsync(int id);
    Task<PostResponse> CreateAsync(PostCreateRequest request);
    Task<PostResponse> UpdateAsync(int id, PostUpdateRequest request);
    Task<PostResponse> UpdatePartialAsync(int id, JsonPathRequest<PostUpdatePartialRequest> request);
    Task DeleteAsync(int id);
    Task SeedDataAsync();
}

