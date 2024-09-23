using AutoMapper;
using static Shared.Dtos.Post.PostDtos;
using Entities = MyBlog.Post.Domain.Entities;

namespace MyBlog.Category.Service.MappingProfiles;

public class PostMappingProfile : Profile
{
    public PostMappingProfile()
    {
        CreateMap<Entities.Post, PostCreateRequest>().ReverseMap();
        CreateMap<Entities.Post, PostUpdateRequest>().ReverseMap();
        CreateMap<Entities.Post, PostUpdatePartialRequest>().ReverseMap();
        CreateMap<Entities.Post, PostListResponse>().ReverseMap();
        CreateMap<Entities.Post, PostResponse>().ReverseMap();
    }
}