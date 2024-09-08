using Contracts.Abstractions.Common;
using MyBlog.Post.Repository;
using MyBlog.Post.Repository.Abstractions;
using static Shared.Dtos.Post.TagDtos;

namespace MyBlog.Post.Service.Abstractions;

public interface ITagService : IBaseService<IRepositoryManager, PostDbContext>
{
    Task<TagResponse> CreateAsync(TagCreateRequest request);
    Task<TagResponse> GetAsync(int id);
    Task<IEnumerable<TagResponse>> GetListAsync(TagListRequest request);
    Task<TagResponse> UpdateAsync(int id, TagUpdateRequest category);
    Task DeleteAsync(int id);
    Task<IEnumerable<TagResponse>> GetTagsFromPost(int postId);
    Task RemoveTagFromPostsAsync(int categoryId, int postId);
}