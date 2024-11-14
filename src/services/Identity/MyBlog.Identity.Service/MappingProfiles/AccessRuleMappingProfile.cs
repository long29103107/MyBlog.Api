using AutoMapper;
using MyBlog.Identity.Domain.Entities;
using Shared.Dtos.Identity.Seed;

namespace MyBlog.Identity.Service.MappingProfiles;

public class AccessRuleMappingProfile : Profile
{
    public AccessRuleMappingProfile()
    {

        CreateMap<AccessRule, AccessRule>().ReverseMap();
    }
}

