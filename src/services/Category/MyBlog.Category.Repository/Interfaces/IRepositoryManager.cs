using Microsoft.EntityFrameworkCore;
using MyBlog.Shared.RepositoryEF.Interfaces;
using Entities = MyBlog.Category.Domain.Entities;

namespace MyBlog.Category.Repository.Interfaces;

public interface IRepositoryManager : IRepositoryManagerBase<CategoryDbContext>
{
    public ICategoryRepository Category { get; }

    DbSet<Entities.Category> Categories { get; }
}