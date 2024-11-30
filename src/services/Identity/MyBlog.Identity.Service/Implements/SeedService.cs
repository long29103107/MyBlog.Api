using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyBlog.Identity.Domain.Entities;
using MyBlog.Identity.Repository;
using MyBlog.Identity.Repository.Abstractions;
using MyBlog.Identity.Service.Abstractions;
using MyBlog.Identity.Service.DependencyInjection.Extensions;
using Newtonsoft.Json;
using Serilog;
using System.Data;
using System.Linq;
using System.Reflection;
using static Shared.Dtos.Identity.SeedDtos;

namespace MyBlog.Identity.Service.Implements;

public class SeedService : BaseIdentityService, ISeedService
{
    public SeedService(IRepositoryManager repoManager, IMapper mapper, IValidatorFactory validatorFactory, ILogger logger)
        : base(repoManager, mapper, validatorFactory, logger)
    {
    }

    public async Task SeedDataAsync(SeedDataRequest request)
    {
        if (request.IsReset)
        {
            try
            {
                await _RemoveCurrentSecurityAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                throw;
            }
        }

        if (request.IsSeed)
        {
            await _ReSeedSecurityAsync();
        }
    }

    private async Task _ReSeedSecurityAsync()
    {
        var logFormat = new List<string>();
        var operationRequests = await _ReadSeedJsonFileAsync<OperationRequest>("operations");
        var permissionRequests = await _ReadSeedJsonFileAsync<PermissionRequest>("permissions");
        var roleRequests = await _ReadSeedJsonFileAsync<RoleRequest>("roles");
        var scopeRequests = await _ReadSeedJsonFileAsync<ScopeRequest>("scopes");

        logFormat.Add("=====================Add Operation=====================");
        var operations = await _CreateOrUpdateOperationAsync(operationRequests);
        operations.ForEach(x => logFormat.Add($"Operation {x}"));

        logFormat.Add("=====================Add Scope=====================");
        var scopes = await _CreateOrUpdateScopeAsync(scopeRequests);
        operations.ForEach(x => logFormat.Add($"Scope {x}"));
        await _repoManager.SaveAsync();
        _repoManager.DetachEntities();

        logFormat.Add("=====================Add Permission=====================");
        var permissions = await _CreateOrUpdatePermissionAsync(
            operations.Select(x => x.Code).Distinct().ToList() ?? new List<string>(), 
            scopes.Select(x => x.Code).Distinct().ToList() ?? new List<string>());
        permissions.ForEach(x => logFormat.Add($"Permission {x}"));

        logFormat.Add("=====================Add Role=====================");
        var roles = await _CreateOrUpdateRoleAsync(roleRequests);
        roles.ForEach(x => logFormat.Add($"Role {x}"));
        await _repoManager.SaveAsync();

        await _AddNewRolePermissionAsync(roles, permissions);
        await _AddNewOperationPermissionAsync(operations, permissions, permissionRequests);

        var accessRules = (from p in permissions
                           from r in roles

                           select new AccessRule
                           {
                               PermissionId = p.Id,
                               RoleId = r.Id,
                           });

        var existingAccessRules = await _repoManager.AccessRule.FindAll().ToListAsync();

        existingAccessRules = existingAccessRules.Where(x =>
            accessRules.Any(y => x.RoleId == y.RoleId && x.PermissionId == y.PermissionId))
            .ToList();

        if (!existingAccessRules.IsNullOrEmpty())
        {
            var accRulesNeedToUpdate = accessRules.Where(x => existingAccessRules.Any(
                y => x.RoleId == y.RoleId && x.PermissionId == y.PermissionId))
                .ToList();

            foreach (var existingAccessRule in existingAccessRules)
            {
                var accRuleNeedToUpdate = accRulesNeedToUpdate.FirstOrDefault(x =>
                    x.RoleId == existingAccessRule.RoleId &&
                    x.PermissionId == existingAccessRule.PermissionId));

                _mapper.Map<AccessRule, AccessRule>(accRuleNeedToUpdate!, existingAccessRule);
            }

            _repoManager.AccessRule.UpdateRange(existingAccessRules);
        }

        var newAccessRules = accessRules.ToList();
        newAccessRules.RemoveAll(x => existingAccessRules.Any(y =>
            x.RoleId == y.RoleId &&
            x.PermissionId == y.PermissionId &&
            x.OperationId == y.OperationId));

        _repoManager.AccessRule.AddRange(newAccessRules);

        try
        {
            await _repoManager.SaveAsync();
        }
        catch (Exception ex)
        {
            _logger.Error(ex, ex.Message);
            throw;
        }
    }

    private async Task _RemoveCurrentSecurityAsync()
    {
        await _repoManager.TruncateAsync(nameof(MyIdentityDbContext.AccessRules));
        await _repoManager.TruncateAsync(nameof(MyIdentityDbContext.Scopes));
        await _repoManager.TruncateAsync(nameof(MyIdentityDbContext.RolePermissions));
        await _repoManager.TruncateAsync(nameof(MyIdentityDbContext.Operations));
        await _repoManager.TruncateAsync(nameof(MyIdentityDbContext.Permissions));
        await _repoManager.TruncateAsync(nameof(MyIdentityDbContext.Roles));
    }

    private async Task<List<T>> _ReadSeedJsonFileAsync<T>(string fileName)
    {
        var result = new List<T>();
        var rootPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        var fullPath = Path.Combine(rootPath, $"Seeds/{fileName}.json");
        using (StreamReader r = new StreamReader(fullPath))
        {
            string json = await r.ReadToEndAsync();
            result = JsonConvert.DeserializeObject<List<T>>(json);
        }

        return result;
    }

    private async Task<List<Scope>> _CreateOrUpdateScopeAsync(List<ScopeRequest> requests)
    {
        var newRequest = new List<ScopeRequest>(requests);
        var existingScopes = await _repoManager.Scope.FindAll().ToListAsync();

        //Case delete
        var scopeNeedToDelete = existingScopes.Where(x => !newRequest.Select(y => y.Code).Distinct().Contains(x.Code));

        _repoManager.Scope.RemoveRange(scopeNeedToDelete);
        existingScopes.RemoveAll(x => scopeNeedToDelete.Select(y => y.Code).Distinct().Contains(x.Code));

        //Case update
        foreach (var existingScope in existingScopes)
        {
            var request = newRequest.FirstOrDefault(x => x.Code == existingScope.Code);
            if (request == null) continue;

            _mapper.Map<ScopeRequest, Scope>(request, existingScope);
        }

        //Case create
        newRequest.RemoveAll(x => existingScopes.Select(y => y.Code).Contains(x.Code));
        var scopeNeedToCreate = _mapper.Map<List<Scope>>(newRequest);
        _repoManager.Scope.AddRange(scopeNeedToCreate);

        return existingScopes.Union(scopeNeedToCreate).ToList();
    }

    private async Task<List<Operation>> _CreateOrUpdateOperationAsync(List<OperationRequest> requests)
    {
        var newRequest = new List<OperationRequest>(requests);
        var existingOperations = await _repoManager.Operation.FindAll().ToListAsync();

        //Case delete
        var operationNeedToDelete = existingOperations.Where(x => !newRequest.Select(y => y.Code).Distinct().Contains(x.Code));
        _repoManager.Operation.RemoveRange(operationNeedToDelete);
        existingOperations.RemoveAll(x => operationNeedToDelete.Select(y => y.Code).Distinct().Contains(x.Code));

        //Case update
        foreach (var existingOperation in existingOperations)
        {
            var request = newRequest.FirstOrDefault(x => x.Code == existingOperation.Code);
            if (request == null) continue;

            _mapper.Map<OperationRequest, Operation>(request, existingOperation);
        }

        //Case create
        newRequest.RemoveAll(x => existingOperations.Select(y => y.Code).Contains(x.Code));
        var operationNeedToCreate = _mapper.Map<List<Operation>>(newRequest);
        _repoManager.Operation.AddRange(operationNeedToCreate);

        return existingOperations.Union(operationNeedToCreate).ToList();
    }

    private async Task<List<Permission>> _CreateOrUpdatePermissionAsync(
        List<string> operationCodes, 
        List<string> scopeCodes)
    {
        var newRequests = (from op in operationCodes
                           from sc in scopeCodes
                           select new PermissionSeedResponse
                           {
                               OperationCode = op,
                               ScopeCode = sc,
                           }).ToList();

        var existingPermissions = await _repoManager.Permission.FindAll()
            .Include(x => x.Operation)
            .Include(x => x.Scope)
            .ToListAsync();

        //Case delete
        var permissionNeedToDelete = existingPermissions
            .Where(x =>
                !operationCodes.Contains(x.Operation.Code) 
                || !scopeCodes.Contains(x.Scope.Code))
            .ToList();

        _repoManager.Permission.RemoveRange(permissionNeedToDelete);
        existingPermissions.RemoveAll(x => permissionNeedToDelete.Any(y => x.Id == y.Id));

        //Case create
        newRequests.RemoveAll(x => existingPermissions.Any(y => y.Operation.Code.Equals(x.OperationCode))
            && existingPermissions.Any(y => y.Scope.Code.Equals(x.ScopeCode)));

        var permissionNeedToCreate = _mapper.Map<List<Permission>>(newRequests);
        _repoManager.Permission.AddRange(permissionNeedToCreate);

        return existingPermissions.Union(permissionNeedToCreate).ToList();
    }

    private async Task<List<Role>> _CreateOrUpdateRoleAsync(List<RoleRequest> requests)
    {
        var newRequests = new List<RoleRequest>(requests);
        var existingRoles = await _repoManager.Role.FindAll().ToListAsync();

        //Case delete
        var roleNeedToDelete = existingRoles.Where(x => !newRequests.Select(y => y.Code).Distinct().Contains(x.Code));
        _repoManager.Role.RemoveRange(roleNeedToDelete);
        existingRoles.RemoveAll(x => roleNeedToDelete.Select(y => y.Code).Distinct().Contains(x.Code));

        //Case update
        foreach (var existingRole in existingRoles)
        {
            var request = newRequests.FirstOrDefault(x => x.Code == existingRole.Code);
            if (request == null) continue;

            _mapper.Map<RoleRequest, Role>(request, existingRole);
        }

        //Case create
        newRequests.RemoveAll(x => existingRoles.Select(y => y.Code).Contains(x.Code));
        var roleNeedToCreate = _mapper.Map<List<Role>>(newRequests);
        _repoManager.Role.AddRange(roleNeedToCreate);

        return existingRoles.Union(roleNeedToCreate).ToList();
    }

    private async Task _AddNewRolePermissionAsync(List<Role> roles, List<Permission> permissions)
    {
        var rolePermissions = await (from role in _repoManager.Role.FindByCondition(x => roles.Select(y => y.Code).Distinct().Contains(x.Code))

                                     join rolePer in _repoManager.RolePermission.FindAll()
                                     on role.Id equals rolePer.RoleId

                                     join per in _repoManager.Permission.FindByCondition(x => permissions.Select(y => y.Code).Distinct().Contains(x.Code))
                                    on rolePer.PermissionId equals per.Id

                                     select rolePer)
                        .Include(x => x.Role)
                        .Include(x => x.Permission)
                        .ToListAsync();

        foreach (var role in roles)
        {
            foreach (var permission in permissions)
            {
                if (rolePermissions.Exists(x => x.Permission.Code == permission.Code && x.Role.Code == role.Code))
                    continue;

                _repoManager.RolePermission.Add(new RolePermission()
                {
                    RoleId = role.Id,
                    PermissionId = permission.Id
                });
            }
        }
    }

    private async Task _AddNewOperationPermissionAsync(List<Operation> operations, List<Permission> permissions, List<PermissionRequest> permissionRequests)
    {
        var operationPermissions = await (from ope in _repoManager.Operation.FindByCondition(x => operations.Select(y => y.Code).Distinct().Contains(x.Code))

                                          join opePer in _repoManager.Scope.FindAll()
                                          on ope.Id equals opePer.OperationId

                                          join per in _repoManager.Permission.FindByCondition(x => permissions.Select(y => y.Code).Distinct().Contains(x.Code))
                                         on opePer.PermissionId equals per.Id

                                          select opePer)
                                   .Include(x => x.Permission)
                                   .Include(x => x.Operation)
                                   .ToListAsync();

        var newOperationPermissions = new List<OperationPermission>();

        foreach (var permission in permissions)
        {
            var children = permissionRequests.FirstOrDefault(x => x.Code == permission.Code)?.Children ?? new();

            foreach (var operation in operations)
            {
                var existingOpePer = operationPermissions.FirstOrDefault(x => x.Permission.Code == permission.Code && x.Operation.Code == operation.Code);

                if (existingOpePer is not null && !(children.Contains(operation.Code)))
                {
                    _repoManager.Scope.Remove(existingOpePer);
                }

                if (children.Contains(operation.Code) && existingOpePer is null)
                {
                    newOperationPermissions.Add(new OperationPermission()
                    {
                        OperationId = operation.Id,
                        PermissionId = permission.Id
                    });
                }

            }
        }
        _repoManager.Scope.AddRange(newOperationPermissions);
    }
}

