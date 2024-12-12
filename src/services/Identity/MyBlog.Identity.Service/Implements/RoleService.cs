using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contracts.Domain.Exceptions.Abtractions;
using FilteringAndSortingExpression.Extensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MyBlog.Identity.Domain.Entities;
using MyBlog.Identity.Domain.Exceptions;
using MyBlog.Identity.Repository.Abstractions;
using MyBlog.Identity.Service.Abstractions;
using MyBlog.Shared.Lib;
using Serilog;
using static Shared.Dtos.Identity.Permission.PermissionDtos;
using static Shared.Dtos.Identity.RoleDtos;
using MyBlog.Shared.Lib.Extensions;
using Exceptions = Contracts.Domain.Exceptions;
using Contracts.Domain.Exceptions;

namespace MyBlog.Identity.Service.Implements;
public class RoleService : BaseIdentityService, IRoleService
{
    
    public RoleService(IRepositoryManager repoManager, IMapper mapper, IValidatorFactory validatorFactory, ILogger logger) : base(repoManager, mapper, validatorFactory, logger)
    {
    }

    #region Get
    public async Task<RoleResponse> GetAsync(int id)
    {
        var role = await _GetRoleAsync(id)
           ?? throw new RoleException.NotFound(id);

        return _mapper.Map<RoleResponse>(role);
    }
    #endregion

    #region Get Active Role
    public async Task<RoleResponse> GetActiveAsync(int id)
    {
        var role = await _GetActiveRoleAsync(id)
           ?? throw new RoleException.NotFound(id);

        return _mapper.Map<RoleResponse>(role);
    }

    private async Task<Role> _GetActiveRoleAsync(int id)
    {
        return await _repoManager.Role.FirstOrDefaultAsync(x => x.Id == id);
    }
    #endregion

    #region Get List
    public async Task<IEnumerable<RoleResponse>> GetListAsync(RoleListRequest request)
    {
        var result = await _RoleIgnoreGlobalFilter()
           .Filter(request)
           .ProjectTo<RoleResponse>(_mapper.ConfigurationProvider)
           .ToListAsync();

        return result;
    }
    #endregion

    #region Create
    public async Task<RoleResponse> CreateAsync(RoleCreateRequest request)
    {
        var isExistingRole = await _RoleIgnoreGlobalFilter()
            .FirstOrDefaultAsync(x => x.Code == request.Code || x.Name == request.Name);

        if (isExistingRole is not null)
        {
            throw new RoleException.ExistingRole(request.Code, request.Name);
        }

        var newRole = _mapper.Map<Role>(request);
        _repoManager.Role.Add(newRole);
        await _repoManager.SaveAsync();
        _repoManager.Role.Detach(newRole);

        //Add permission and operation for role
        var permissions = await _repoManager.Permission.FindAll().ToListAsync();
       
        //Add AccessRule
        _AddAccessRuleForNewRole(newRole, permissions);
        await _repoManager.SaveAsync();

        return _mapper.Map<RoleResponse>(newRole);
    }


    private void _AddAccessRuleForNewRole(Role newRole, IList<Permission> permissions)
    {
        foreach (var permission in permissions)
        {
            var newAccessRule = new AccessRule()
            {
                RoleId = newRole.Id,
                PermissionId = permission.Id,
            };

            _repoManager.AccessRule.Add(newAccessRule);
        }
    }
    #endregion

    #region Update
    public async Task<RoleResponse> UpdateAsync(int id, RoleUpdateRequest request)
    {
        var role = await _GetRoleAsync(id)
          ?? throw new RoleException.NotFound(id);

        _mapper.Map<RoleUpdateRequest, Role>(request, role);



        _repoManager.Role.Update(role);
        await _repoManager.SaveAsync();

        return _mapper.Map<RoleResponse>(role);
    }
    #endregion

    #region Update Partial
    public async Task<RoleResponse> UpdatePartialAsync(int id, JsonPathRequest<RoleUpdatePartialRequest> request)
    {
        var role = await _GetRoleAsync(id)
          ?? throw new RoleException.NotFound(id);

        //if (role.IsLocked)
        //{
        //    throw new BadRequestException("The role has created from system, cannot update this one!");
        //}

        //var isExistingRole = await _RoleIgnoreGlobalFilter().Where(x =>
        //       x.Code.Equals(role.Code) || x.Name.Equals(role.Name))
        //   .AnyAsync();

        //if (isExistingRole)
        //{
        //    throw new BadRequestException("Existing role has code or name!");
        //}

        request.ApplyTo(role, _mapper);



        _repoManager.Role.Update(role);
        await _repoManager.SaveAsync();

        return _mapper.Map<RoleResponse>(role);
    }
    #endregion

    #region Delete
    public async Task<bool> DeleteAsync(int id)
    {
        var role = await _GetRoleAsync(id)
            ?? throw new RoleException.NotFound(id);

        _CheckLock(role.IsLocked);

        _repoManager.Role.Remove(role);
        try
        {
            await _repoManager.SaveAsync();
        }
        catch (Exception ex)
        {
            _logger.Error(ex, ex.Message);
            return false;
        }

        return true;
    }
    #endregion

    #region Get Permissions By Role
    public async Task<IEnumerable<PermissionResponse>> GetPermissionsByRoleAsync(int roleId)
    {
        var role = await _GetRoleAsync(roleId)
           ?? throw new RoleException.NotFound(roleId);

        var result = await (from p in _repoManager.Permission.FindAll()

                            join ac in _repoManager.AccessRule.FindAll()
                            on p.Id equals ac.PermissionId

                            join r in _repoManager.Role.FindAll()
                            on ac.RoleId equals r.Id

                            where r.Id == roleId

                            select p)
                    .ProjectTo<PermissionResponse>(_mapper.ConfigurationProvider)
                    .ToListAsync();

        return result;
    }
    #endregion

    #region Get Permission By Role
    public async Task<PermissionResponse> GetPermissionByRoleAsync(int roleId, int permissionId)
    {
        var role = await _GetRoleAsync(roleId)
           ?? throw new RoleException.NotFound(roleId);

        var result = await (from p in _repoManager.Permission.FindAll()

                            join ac in _repoManager.AccessRule.FindAll()
                            on p.Id equals ac.PermissionId

                            join r in _repoManager.Role.FindAll()
                            on ac.RoleId equals r.Id

                            where r.Id == roleId

                            select p)
                    .ProjectTo<PermissionResponse>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync()
                     ?? throw new PermissionException.NotFound(roleId); ;

        return result;
    }
    #endregion

    #region Common Private
    private async Task<Role> _GetRoleAsync(int id)
    {
        return await _RoleIgnoreGlobalFilter().FirstOrDefaultAsync(x => x.Id == id); 
    }

    public IQueryable<Role> _RoleIgnoreGlobalFilter()
    {
        return _repoManager.Role.FindAll().IgnoreQueryFilters();
    }

    private async Task _ValidateRoleAsync(Role model)
    {
        var validator = _validatorFactory.GetValidator<Role>();
        var result = await validator.ValidateAsync(model);

        if (!result.IsValid)
        {
            var errors = result.Errors.Select(x => new ValidationError(x.PropertyName, x.ErrorMessage)).ToList();

            throw new Exceptions.ValidationException(errors);
        }
    }

    private void _CheckLock(bool isLocked)
    {
        if (isLocked)
        {
            throw new BadRequestException("The role has created from system, cannot update this one!");
        }
    }

    #endregion
}
