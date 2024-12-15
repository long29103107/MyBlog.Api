using MyBlog.Identity.Domain.Entities;

namespace MyBlog.Identity.Repository.Abstractions;

public interface IUserRoleRepository : IRepositoryIdentityBase<UserRole, MyIdentityDbContext>
{ }