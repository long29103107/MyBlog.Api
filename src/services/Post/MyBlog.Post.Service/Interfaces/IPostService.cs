using MyBlog.Post.Repository.Implements;
using MyBlog.Shared.Dtos.Post;
using MyBlog.Shared.Lib;
using MyBlog.Shared.ServiceBase.Abstractions;

namespace MyBlog.Post.Service.Interfaces;

public interface IPostService : IBaseService<RepositoryManager>
{
    Task<List<PostListResponse>> GetListAsync();
    Task<PostResponse> GetAsync(int id);
    Task<PostResponse> CreateAsync(PostCreateRequest request);
    Task<PostResponse> UpdateAsync(int id, PostUpdateRequest request);
    Task<PostResponse> UpdatePartialAsync(int id, JsonPathRequest<PostUpdatePartialRequest> request);
    Task SeedDataAsync();
}

