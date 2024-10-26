using Entities = MyBlog.Identity.Domain.Entities;
using Contracts.Abstractions.Common;
using MyBlog.Identity.Repository;
using Microsoft.AspNetCore.Identity;
using MyBlog.Identity.Domain.Entities;

namespace MyBlog.Identity.Repository.Abstractions;

public interface IUserRoleRepository : IRepositoryBase<UserRole, MyIdentityDbContext>
{
}