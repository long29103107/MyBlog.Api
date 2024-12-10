using MyBlog.Identity.Domain.Entities;

namespace MyBlog.Identity.Repository.Abstractions;

public interface IUserRepository : IRepositoryIdentityBase<User, MyIdentityDbContext>
{ }