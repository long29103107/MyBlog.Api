using Microsoft.EntityFrameworkCore;
using MyBlog.Category.Repository.Interfaces;
using MyBlog.Shared.RepositoryEF.Implements;
using MyBlog.Shared.RepositoryEF.Interfaces;
using Entities = MyBlog.Category.Domain.Entities;

namespace MyBlog.Category.Repository.Implements;
public class RepositoryManager : RepositoryManagerBase<CategoryDbContext>, IRepositoryManager
{
    public RepositoryManager(IUnitOfWork<CategoryDbContext> unitOfWork, CategoryDbContext context) : base(unitOfWork, context)
    {
    }
    private ICategoryRepository _category;

    public ICategoryRepository Category
    {
        get
        {
            if (_category == null)
            {
                _category = new CategoryRepository(_context, _unitOfWork);
            }

            return _category;
        }
    }

    public DbSet<Entities.Category> Categories
    {
        get
        {
            return _context.Categories;
        }
    }
}
