using Entities = MyBlog.Post.Domain.Entities;
using Contracts.Abstractions.Common;

namespace MyBlog.Post.Repository.Abstractions;

public interface IPostRepository : IRepositoryBase<Entities.Post, PostDbContext>
{
}