using Contracts.Abstractions.Common;
using Infrastructures.Common;
using Microsoft.EntityFrameworkCore;
using MyBlog.Post.Repository.Abstractions;
using Entities = MyBlog.Post.Domain.Entities;

namespace MyBlog.Post.Repository.Implements;
public class RepositoryManager : UnitOfWork<PostDbContext>, IRepositoryManager
{
    public RepositoryManager(PostDbContext context) : base(context)
    {
    }

    private IPostRepository _post;

    public IPostRepository Post
    {
        get
        {
            if (_post == null)
            {
                _post = new PostRepository(_context);
            }

            return _post;
        }
    }

    public DbSet<Entities.Post> Posts
    {
        get
        {
            return _context.Posts;
        }
    }
}
