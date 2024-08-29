using MyBlog.Post.Repository.Interfaces;
using MyBlog.Shared.RepositoryEF.Implements;
using MyBlog.Shared.RepositoryEF.Interfaces;
using Entities = MyBlog.Post.Domain.Entities;

namespace MyBlog.Post.Repository.Implements;

public class PostRepository : RepositoryBase<Entities.Post, PostDbContext, int>, IPostRepository
{
    public PostRepository(PostDbContext context, IUnitOfWork<PostDbContext> unitOfWork)
        : base(context, unitOfWork)
    {
    }

    public override void BeforeAdd(Entities.Post entity)
    {
        entity.CreatedBy = "unknown";
        entity.CreatedAt = DateTimeOffset.UtcNow;
    }
    public override void BeforeUpdate(Entities.Post entity)
    {
        entity.UpdatedBy = "unknown";
        entity.UpdatedAt = DateTimeOffset.UtcNow;
    }
}