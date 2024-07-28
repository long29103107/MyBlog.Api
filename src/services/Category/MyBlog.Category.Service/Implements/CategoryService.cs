using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyBlog.Category.Repository.Interfaces;
using MyBlog.Category.Service.Interfaces;
using MyBlog.Shared.Databases.Category;
using MyBlog.Shared.ServiceBase.Implements;
using Entities = MyBlog.Category.Domain.Entities;

namespace MyBlog.Category.Service.Implements;

public class CategoryService : BaseService<IRepositoryManager>, ICategoryService
{
    public CategoryService(IRepositoryManager repoManager, IMapper mapper)//, ILogger logger)
                                                            : base(repoManager, mapper)//, logger)
    {
    }

    public async Task<List<ListCategoryResponse>> GetListAsync()
    {
        var categories = await _repoManager.Category.FindAll().ToListAsync();

        var result = _mapper.Map<List<ListCategoryResponse>>(categories);

        return result;
    }

    public async Task SeedDataAsync()
    {
        if(!await _repoManager.Category.AnyAsync())
        {
            var index = 0;
            var categories = new List<Entities.Category>();

            while (index < 1000)
            {
                categories.Add(new Entities.Category()
                {
                    Name = "Category " + (index + 1),
                    SlugName = "category-" + (index + 1)
                });
                index++;
            }

            _repoManager.Category.AddRange(categories);
            await _repoManager.SaveAsync();
        }
    }
}