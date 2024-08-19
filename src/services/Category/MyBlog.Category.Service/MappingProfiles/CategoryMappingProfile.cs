using AutoMapper;
using MyBlog.Shared.Databases.Category;
using MyBlog.Shared.Dtos.Category;
using Entities = MyBlog.Category.Domain.Entities;

namespace MyBlog.Category.Service.MappingProfiles;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        ModelToResponse();
        RequestToModel();
    }

    private void RequestToModel()
    {
        CreateMap<CreateCategoryRequest, Entities.Category>().ReverseMap();
        CreateMap<UpdateCategoryRequest, Entities.Category>().ReverseMap();
        CreateMap<UpdatePartialCategoryRequest, Entities.Category>().ReverseMap();
    }

    private void ModelToResponse()
    {
        CreateMap<Entities.Category, ListCategoryResponse>().ReverseMap();
        CreateMap<Entities.Category, CategoryResponse>().ReverseMap();
    }
}