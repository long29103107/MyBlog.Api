using Contracts.Abstractions.Common;
using Contracts.Abstractions.Shared;
using Contracts.Dtos;
using MyBlog.Post.Repository;
using MyBlog.Post.Repository.Implements;
using MyBlog.Shared.Lib;
using static Shared.Dtos.Post.PostDtos;

namespace MyBlog.Post.Service.Abstractions;

public interface IPostService : IBaseService<RepositoryManager, PostDbContext>
{
    Task<ResponseListResult<PostListResponse>> GetListAsync(PostListRequest request);
    Task<PagingListResponse<PostResponse>> GetPagedListAsync(PagingPostRequest request);
    Task<ResponseResult<PostResponse>> GetAsync(int id);
    Task<ResponseResult<PostResponse>> CreateAsync(PostCreateRequest request);
    Task<ResponseResult<PostResponse>> UpdateAsync(int id, PostUpdateRequest request);
    Task<ResponseResult<PostResponse>> UpdatePartialAsync(int id, JsonPathRequest<PostUpdatePartialRequest> request);
    Task<ResponseResult> DeleteAsync(int id);
}

