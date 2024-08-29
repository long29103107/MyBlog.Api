using AutoMapper;
using MyBlog.Shared.Dtos.Post;
using Entities = MyBlog.Post.Domain.Entities;

namespace MyBlog.Category.Service.MappingProfiles;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<PostCreateRequest, Entities.Post>().ReverseMap();
        CreateMap<PostUpdateRequest, Entities.Post>().ReverseMap();
        CreateMap<PostUpdatePartialRequest, Entities.Post>().ReverseMap();
        CreateMap<Entities.Post, PostListResponse>().ReverseMap();
        CreateMap<Entities.Post, PostResponse>().ReverseMap();
    }
}