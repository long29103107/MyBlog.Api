using Microsoft.EntityFrameworkCore;
using MyBlog.Post.Repository.Interfaces;
using MyBlog.Shared.RepositoryEF.Implements;
using MyBlog.Shared.RepositoryEF.Interfaces;
using Entities = MyBlog.Post.Domain.Entities;

namespace MyBlog.Post.Repository.Implements;
public class RepositoryManager : RepositoryManagerBase<PostDbContext>, IRepositoryManager
{
    public RepositoryManager(IUnitOfWork<PostDbContext> unitOfWork, PostDbContext context) : base(unitOfWork, context)
    {
    }
    private IPostRepository _post;

    public IPostRepository Post
    {
        get
        {
            if (_post == null)
            {
                _post = new PostRepository(_context, _unitOfWork);
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
