using AutoMapper;
using MyBlog.Identity.Domain.Entities;
using static Shared.Dtos.Identity.RoleDtos;

namespace MyBlog.Identity.Service.MappingProfiles;

public class RoleMappingProfile : Profile
{
    public RoleMappingProfile()
    {
        CreateMap<Role, RoleCreateRequest>().ReverseMap();
        CreateMap<Role, RoleUpdateRequest>().ReverseMap();
        CreateMap<Role, RoleUpdatePartialRequest>().ReverseMap();
        CreateMap<Role, RoleListResponse>().ReverseMap();
        CreateMap<Role, RoleResponse>().ReverseMap();
    }
}

