using Contracts.Abstractions.Common;
using MyBlog.Post.Repository;
using MyBlog.Post.Repository.Abstractions;
using MyBlog.Shared.Lib;
using static Shared.Dtos.Post.PostDtos;

namespace MyBlog.Post.Service.Abstractions;

public interface IPostService : IBaseService<IRepositoryManager, PostDbContext>
{
    Task<List<PostListResponse>> GetListAsync(PostListRequest request);
    Task<List<PostListResponse>> GetPagedListAsync(PagingPostRequest request);
    Task<PostResponse> GetAsync(int id);
    Task<PostResponse> CreateAsync(PostCreateRequest request);
    Task<PostResponse> UpdateAsync(int id, PostUpdateRequest request);
    Task<PostResponse> UpdatePartialAsync(int id, JsonPathRequest<PostUpdatePartialRequest> request);
    Task<bool> DeleteAsync(int id);
}

