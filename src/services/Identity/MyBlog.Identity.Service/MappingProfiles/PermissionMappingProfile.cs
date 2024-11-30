using AutoMapper;
using MyBlog.Identity.Domain.Entities;
using static Shared.Dtos.Identity.SeedDtos;

namespace MyBlog.Identity.Service.MappingProfiles;

public class PermissionMappingProfile : Profile
{
    public PermissionMappingProfile()
    {
        CreateMap<Permission, PermissionRequest>().ReverseMap();
        CreateMap<Scope, PermissionRequest>().ReverseMap();
    }
}

