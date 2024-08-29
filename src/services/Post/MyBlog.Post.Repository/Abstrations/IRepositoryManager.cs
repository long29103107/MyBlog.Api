using Microsoft.EntityFrameworkCore;
using MyBlog.Shared.RepositoryEF.Interfaces;
using Entities = MyBlog.Post.Domain.Entities;

namespace MyBlog.Post.Repository.Interfaces;

public interface IRepositoryManager : IRepositoryManagerBase<PostDbContext>
{
    public IPostRepository Post { get; }

    DbSet<Entities.Post> Posts { get; }
}