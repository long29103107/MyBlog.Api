using AutoMapper;
using MyBlog.Identity.Domain.Entities;
using Shared.Dtos.Identity.Seed;

namespace MyBlog.Identity.Service.MappingProfiles;

public class OperationMappingProfile : Profile
{
    public OperationMappingProfile()
    {

        CreateMap<Operation, OperationRequest>().ReverseMap();
    }
}

