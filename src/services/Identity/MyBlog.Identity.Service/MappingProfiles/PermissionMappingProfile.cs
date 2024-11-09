using AutoMapper;
using MyBlog.Identity.Domain.Entities;
using Shared.Dtos.Identity.Seed;

namespace MyBlog.Identity.Service.MappingProfiles;

public class PermissionMappingProfile : Profile
{
    public PermissionMappingProfile()
    {
        CreateMap<Permission, PermissionRequest>().ReverseMap();
    }
}

