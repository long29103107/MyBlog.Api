using MyBlog.Contracts.Domains.ValueOf;
using MyBlog.Shared.RepositoryEF.Interfaces;
using Entities = MyBlog.Category.Domain.Entities;

namespace MyBlog.Category.Repository.Interfaces;

public interface ICategoryRepository : IRepositoryBase<Entities.Category, CategoryDbContext, CategoryId>
{
}