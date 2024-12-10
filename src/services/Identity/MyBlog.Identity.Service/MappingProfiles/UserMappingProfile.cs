using AutoMapper;
using MyBlog.Identity.Domain.Entities;
using static Shared.Dtos.Identity.UserDtos;
namespace MyBlog.Identity.Service.MappingProfiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserCreateRequest>().ReverseMap();  
        CreateMap<User, UserUpdateRequest>().ReverseMap();
        CreateMap<User, UserUpdatePartialRequest>().ReverseMap();
        CreateMap<User, UserListResponse>().ReverseMap();
        CreateMap<User, UserResponse>().ReverseMap();
    }
}

