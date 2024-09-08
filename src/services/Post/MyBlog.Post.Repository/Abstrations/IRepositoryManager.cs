using Contracts.Abstractions.Common;
using Microsoft.EntityFrameworkCore;
using Entities = MyBlog.Post.Domain.Entities;

namespace MyBlog.Post.Repository.Abstractions;

public interface IRepositoryManager : IUnitOfWork<PostDbContext>
{
    public IPostRepository Post { get; }
    public ICategoryRepository Category { get; }
    public ITagRepository Tag { get; }

    DbSet<Entities.Post> Posts { get; }

    DbSet<Entities.Category> Categories { get; }
    DbSet<Entities.Tag> Tags { get; }
}