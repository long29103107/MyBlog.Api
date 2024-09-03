using Contracts.Abstractions.Common;
using Contracts.Abstractions.Shared;
using MyBlog.Post.Repository.Implements;
using MyBlog.Shared.Lib;
using static Shared.Dtos.Post.PostDtos;

namespace MyBlog.Post.Service.Abstractions;

public interface IPostService : IBaseService<RepositoryManager>
{
    Task<Response<List<PostListResponse>>> GetListAsync();
    //Task<PagedList<PostListResponse>> GetPagedListAsync();
    Task<Response<PostResponse>> GetAsync(int id);
    Task<Response<PostResponse>> CreateAsync(PostCreateRequest request);
    Task<Response<PostResponse>> UpdateAsync(int id, PostUpdateRequest request);
    Task<Response<PostResponse>> UpdatePartialAsync(int id, JsonPathRequest<PostUpdatePartialRequest> request);
    Task<Response> DeleteAsync(int id);
    Task SeedDataAsync();
}

