using AutoMapper;
using static Shared.Dtos.Post.TagDtos;
using Entities = MyBlog.Post.Domain.Entities;

namespace MyBlog.Post.Service.MappingProfiles;

public class TagMappingProfile : Profile
{
	public TagMappingProfile()
	{
        CreateMap<Entities.Tag, TagCreateRequest>().ReverseMap();
        CreateMap<Entities.Tag, TagUpdateRequest>().ReverseMap();
        CreateMap<Entities.Tag, TagUpdatePartialRequest>().ReverseMap();
        CreateMap<Entities.Tag, TagListResponse>().ReverseMap();
        CreateMap<Entities.Tag, TagResponse>().ReverseMap();
    }
}