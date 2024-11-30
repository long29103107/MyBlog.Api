using AutoMapper;
using MyBlog.Identity.Domain.Entities;
using static Shared.Dtos.Identity.SeedDtos;

namespace MyBlog.Identity.Service.MappingProfiles;

public class OperationMappingProfile : Profile
{
    public OperationMappingProfile()
    {

        CreateMap<Operation, OperationRequest>().ReverseMap();
    }
}

