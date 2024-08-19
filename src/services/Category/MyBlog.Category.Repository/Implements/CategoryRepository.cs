using MyBlog.Category.Repository.Interfaces;
using MyBlog.Contracts.Domains.ValueOf;
using MyBlog.Shared.RepositoryEF.Implements;
using MyBlog.Shared.RepositoryEF.Interfaces;
using Entities = MyBlog.Category.Domain.Entities;

namespace MyBlog.Category.Repository.Implements;

public class CategoryRepository : RepositoryBase<Entities.Category, CategoryDbContext, int>, ICategoryRepository
{
    public CategoryRepository(CategoryDbContext context, IUnitOfWork<CategoryDbContext> unitOfWork) 
        : base(context, unitOfWork)
    {
    }

    public override void BeforeAdd(Entities.Category entity)
    {
        entity.CreatedBy = "unknown";
        entity.CreatedAt = DateTimeOffset.UtcNow;
    }
    public override void BeforeUpdate(Entities.Category entity)
    {
        entity.UpdatedBy = "unknown";
        entity.UpdatedAt = DateTimeOffset.UtcNow;
    }
}