using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlog.Category.Repository.Interfaces;
using MyBlog.Category.Service.Interfaces;
using MyBlog.Contracts.Domains.ValueOf;
using MyBlog.Shared.Databases.Category;
using MyBlog.Shared.Dtos.Category;
using MyBlog.Shared.Lib;
using MyBlog.Shared.ServiceBase.Implements;
using MyBlog.Shared.Lib.Extensions;
using Entities = MyBlog.Category.Domain.Entities;
using FluentValidation;

namespace MyBlog.Category.Service.Implements;

public class CategoryService : BaseService<IRepositoryManager>, ICategoryService
{
    private readonly IValidatorFactory _validatorFactory;
    public CategoryService(IRepositoryManager repoManager, IMapper mapper, IValidatorFactory validatorFactory) : base(repoManager, mapper)
    {
        _validatorFactory = validatorFactory;
    }

    public async Task<List<ListCategoryResponse>> GetListAsync()
    {
        var categories = await _repoManager.Category
            .FindAll()
            .ToListAsync();

        var result = _mapper.Map<List<ListCategoryResponse>>(categories);

        return result;
    }

    

    public async Task<CategoryResponse> GetAsync(CategoryId id)
    {
        var category = await _InternalGetCategoryAsync(id);


        //TODO: handle not found

        var result = _mapper.Map<CategoryResponse>(category);

        return result;
    }

    public async Task<CategoryResponse> CreateAsync(CreateCategoryRequest request)
    {
        var model = _mapper.Map<Entities.Category>(request);

        await _ValidateCategoryAsync(model);

        _repoManager.Category.Add(model);
        await _repoManager.SaveAsync();

        return _mapper.Map<CategoryResponse>(model);
    }

    public async Task<CategoryResponse> UpdateAsync(CategoryId id, UpdateCategoryRequest request)
    {
        var model = await _InternalGetCategoryAsync(id);

        //TODO: handle not found

        _mapper.Map<UpdateCategoryRequest, Entities.Category>(request, model!);

        await _ValidateCategoryAsync(model);

        _repoManager.Category.Update(model);

        await _repoManager.SaveAsync();

        return _mapper.Map<CategoryResponse>(model);
    }

    public async Task<CategoryResponse> UpdatePartialAsync(CategoryId id, [FromBody] JsonPathRequest<UpdatePartialCategoryRequest> request)
    {
        var model = await _InternalGetCategoryAsync(id);

        request.ApplyTo(model, _mapper);

        await _ValidateCategoryAsync(model);

        _repoManager.Category.Update(model);

        await _repoManager.SaveAsync();

        return _mapper.Map<CategoryResponse>(model);
    }

    public async Task SeedDataAsync()
    {
        if (!await _repoManager.Category.AnyAsync())
        {
            var index = 0;
            var categories = new List<Entities.Category>();

            while (index < 1000)
            {
                var name = "Category " + (index + 1);
                categories.Add(new Entities.Category()
                {
                    Name = name,
                });
                index++;
            }

            _repoManager.Category.AddRange(categories);
            await _repoManager.SaveAsync();
        }
    }

    private async Task<Entities.Category> _InternalGetCategoryAsync(CategoryId id)
    {
       return await _repoManager.Category
          .FindByCondition(x => x.Id == id)
          .FirstOrDefaultAsync()
          ?? new Entities.Category();
    }

    private async Task _ValidateCategoryAsync(Entities.Category model)
    {
        var validator = _validatorFactory.GetValidator<Entities.Category>();
        var result = await validator.ValidateAsync(model);

        if (!result.IsValid)
        {
            throw new Exception("Invalid Category model");
            //throw new Exception(400,
            //  "error",
            //  string.Join(", ", result.Errors.Select(x => x.ErrorMessage)));
        }  
    }
}