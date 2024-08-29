using MyBlog.Shared.RepositoryEF.Interfaces;
using Entities = MyBlog.Post.Domain.Entities;

namespace MyBlog.Post.Repository.Interfaces;

public interface IPostRepository : IRepositoryBase<Entities.Post, PostDbContext, int>
{
}