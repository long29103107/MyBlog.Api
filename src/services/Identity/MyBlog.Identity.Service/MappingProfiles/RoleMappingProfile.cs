using AutoMapper;
using MyBlog.Identity.Domain.Entities;
using Shared.Dtos.Identity.Seed;

namespace MyBlog.Identity.Service.MappingProfiles;

public class RoleMappingProfile : Profile
{
    public RoleMappingProfile()
    {

        CreateMap<Role, RoleRequest>().ReverseMap();
    }
}

