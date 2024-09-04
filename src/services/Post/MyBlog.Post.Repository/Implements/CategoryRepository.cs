using Infrastructures.Common;
using MyBlog.Post.Repository.Abstractions;
using Entities = MyBlog.Post.Domain.Entities;

namespace MyBlog.Post.Repository.Implements;

public class CategoryRepository : RepositoryBase<Entities.Category, PostDbContext>, ICategoryRepository
{
    public CategoryRepository(PostDbContext context) : base(context)
    {
    }
}