using AutoMapper;
using AutoMapper.QueryableExtensions;
using FilteringAndSortingExpression.Extensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MyBlog.Identity.Domain.Exceptions;
using MyBlog.Identity.Repository.Abstractions;
using MyBlog.Identity.Service.Abstractions;
using Serilog;
using static Shared.Dtos.Identity.Permission.PermissionDtos;
namespace MyBlog.Identity.Service.Implements;
public class PermissionService : BaseIdentityService, IPermissionService
{
    public PermissionService(IRepositoryManager repoManager, IMapper mapper, IValidatorFactory validatorFactory, ILogger logger) 
        : base(repoManager, mapper, validatorFactory, logger)
    {
    }

    public async Task<PermissionResponse> GetAsync(int id)
    {
        var result = await _repoManager.Permission.FindByCondition(x => x.Id == id)
            .ProjectTo<PermissionResponse>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync()
            ?? throw new PermissionException.NotFound(id);

        return result;  
    }

    public async Task<IEnumerable<PermissionResponse>> GetListAsync(PermissionListRequest request)
    {
        var result = await _repoManager.Permission.FindAll()
           .ProjectTo<PermissionResponse>(_mapper.ConfigurationProvider)
           .Filter(request)
           .ToListAsync();

        return result;
    }

    public async Task<bool> HasPermissionAsync(int userId, string permission)
    {
        return true;
    }

    public async Task<bool> HasPermissionAsync(int userId, int permissionId)
    {
        return true;
    }

}
