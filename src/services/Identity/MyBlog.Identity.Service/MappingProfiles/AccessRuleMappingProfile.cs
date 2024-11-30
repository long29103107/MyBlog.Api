using AutoMapper;
using MyBlog.Identity.Domain.Entities;

namespace MyBlog.Identity.Service.MappingProfiles;

public class AccessRuleMappingProfile : Profile
{
    public AccessRuleMappingProfile()
    {

        CreateMap<AccessRule, AccessRule>().ReverseMap();
    }
}

