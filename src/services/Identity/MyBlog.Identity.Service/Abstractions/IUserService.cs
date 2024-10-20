using Contracts.Abstractions.Common;
using MyBlog.Identity.Domain.Entities;
using MyBlog.Identity.Repository;
using MyBlog.Post.Repository.Abstractions;
using System.Collections.Generic;

namespace MyBlog.Identity.Service.Abstractions;

public interface IUserService : IBaseService<IRepositoryManager, MyIdentityDbContext>
{
    Task<IEnumerable<int>> GetUserIdsAsync();
}
