using AutoMapper;
using MyBlog.Shared.Databases.Category;
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
        
    }

    private void ModelToResponse()
    {
        CreateMap<Entities.Category, ListCategoryResponse>().ReverseMap();
    }
}