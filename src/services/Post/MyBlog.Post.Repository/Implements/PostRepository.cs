using Contracts.Abstractions.Common;
using Infrastructures.Common;
using MyBlog.Post.Repository.Abstractions;
using Entities = MyBlog.Post.Domain.Entities;

namespace MyBlog.Post.Repository.Implements;

public class PostRepository : RepositoryBase<Entities.Post, PostDbContext>, IPostRepository
{
    public PostRepository(PostDbContext context) : base(context)
    {
    }
}