using AutoMapper;
using static Shared.Dtos.Category.CategoryDtos;
using Entities = MyBlog.Post.Domain.Entities;

namespace MyBlog.Post.Service.MappingProfiles;

public class CategoryMappingProfile : Profile
{
	public CategoryMappingProfile()
	{
        CreateMap<Entities.Category, CategoryCreateRequest>().ReverseMap();
        CreateMap<Entities.Category, CategoryUpdateRequest>().ReverseMap();
        CreateMap<Entities.Category, CategoryUpdatePartialRequest>().ReverseMap();
        CreateMap<Entities.Category, CategoryListResponse>().ReverseMap();
        CreateMap<Entities.Category, CategoryResponse>().ReverseMap();
    }
}