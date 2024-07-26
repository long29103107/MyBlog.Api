using MyBlog.Category.Repository.Interfaces;
using MyBlog.Shared.RepositoryEF.Implements;
using MyBlog.Shared.RepositoryEF.Interfaces;
using Entities = MyBlog.Category.Domain.Entities;

namespace MyBlog.Category.Repository.Implements;

public class CategoryRepository : RepositoryBase<Entities.Category, CategoryDbContext>, ICategoryRepository
{
    public CategoryRepository(CategoryDbContext context, IUnitOfWork<CategoryDbContext> unitOfWork) : base(context, unitOfWork)
    {
    }
}