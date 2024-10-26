using Entities = MyBlog.Identity.Domain.Entities;
using Contracts.Abstractions.Common;

namespace MyBlog.Identity.Repository.Abstractions;

public interface IPermissionRepository : IRepositoryBase<Entities.Permission, MyIdentityDbContext>
{
}