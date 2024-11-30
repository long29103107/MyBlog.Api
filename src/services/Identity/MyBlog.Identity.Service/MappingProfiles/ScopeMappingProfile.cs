using AutoMapper;
using MyBlog.Identity.Domain.Entities;
using static Shared.Dtos.Identity.SeedDtos;

namespace MyBlog.Identity.Service.MappingProfiles;

public class ScopeMappingProfile : Profile
{
    public ScopeMappingProfile()
    {
        CreateMap<Scope, ScopeRequest>().ReverseMap();
    }
}

