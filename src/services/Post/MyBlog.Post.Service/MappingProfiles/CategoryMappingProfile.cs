using AutoMapper;
using static Shared.Dtos.Category.CategoryDtos;
using Entities = MyBlog.Post.Domain.Entities;

namespace MyBlog.Post.Service.MappingProfiles;

public class CategoryMappingProfile : Profile
{
	public CategoryMappingProfile()
	{
        CreateMap<Entities.Post, CategoryCreateRequest>().ReverseMap();
        CreateMap<Entities.Post, CategoryUpdateRequest>().ReverseMap();
        CreateMap<Entities.Post, CategoryUpdatePartialRequest>().ReverseMap();
        CreateMap<Entities.Post, CategoryListResponse>().ReverseMap();
        CreateMap<Entities.Post, CategoryResponse>().ReverseMap();
    }
}