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
    private ICategoryRepository _category;
    private ITagRepository _tag;

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
    public ICategoryRepository Category
    {
        get
        {
            if (_category == null)
            {
                _category = new CategoryRepository(_context);
            }

            return _category;
        }
    }

    public ITagRepository Tag
    {
        get
        {
            if (_tag == null)
            {
                _tag = new TagRepository(_context);
            }

            return _tag;
        }
    }

    public DbSet<Entities.Post> Posts
    {
        get
        {
            return _context.Posts;
        }
    }
    public DbSet<Entities.Category> Categories
    {
        get
        {
            return _context.Categories;
        }
    }
    public DbSet<Entities.Tag> Tags
    {
        get
        {
            return _context.Tags;
        }
    }
}
