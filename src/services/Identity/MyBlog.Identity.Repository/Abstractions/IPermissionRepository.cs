using Entities = MyBlog.Identity.Domain.Entities;
using Contracts.Abstractions.Common;

namespace MyBlog.Post.Repository.Abstractions;

public interface IPermissionRepository : IRepositoryBase<Entities.Category, PostDbContext>
{
}