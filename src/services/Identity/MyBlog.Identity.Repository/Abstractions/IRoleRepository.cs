using MyBlog.Identity.Domain.Entities;

namespace MyBlog.Identity.Repository.Abstractions;

public interface IRoleRepository : IRepositoryIdentityBase<Role, MyIdentityDbContext>
{ }