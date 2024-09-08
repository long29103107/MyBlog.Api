using Contracts.Abstractions.Common;
using Infrastructures.Common;
using MyBlog.Post.Repository.Abstractions;
using Entities = MyBlog.Post.Domain.Entities;

namespace MyBlog.Post.Repository.Implements;

public class TagRepository : RepositoryBase<Entities.Tag, PostDbContext>, ITagRepository
{
    public TagRepository(PostDbContext context) : base(context)
    {
    }
}