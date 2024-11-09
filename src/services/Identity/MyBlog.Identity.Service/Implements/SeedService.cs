using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MyBlog.Identity.Domain.Entities;
using MyBlog.Identity.Repository;
using MyBlog.Identity.Repository.Abstractions;
using MyBlog.Identity.Service.Abstractions;
using Newtonsoft.Json;
using Serilog;
using Shared.Dtos.Identity.Seed;
using System.Data;
using System.Linq;
using System.Reflection;

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

        if(request.IsSeed)
        {
            var operationRequests = await _ReadSeedJsonFileAsync<OperationRequest>("operations");   
            var permissionRequests = await _ReadSeedJsonFileAsync<PermissionRequest>("permissions");
            var roleRequests = await _ReadSeedJsonFileAsync<RoleRequest>("roles");

            var operations = await _CreateOrUpdateOperationAsync(operationRequests);
            var permissions = await _CreateOrUpdatePermissionAsync(permissionRequests);
            var roles = await _CreateOrUpdateRoleAsync(roleRequests);
            await _repoManager.SaveAsync();

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

            var operationPermissions = await (from ope in _repoManager.Operation.FindByCondition(x => operations.Select(y => y.Code).Distinct().Contains(x.Code))

                                         join opePer in _repoManager.OperationPermission.FindAll()
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
                        _repoManager.OperationPermission.Remove(existingOpePer);
                    }

                    if (children.Contains(operation.Code))
                    {
                        newOperationPermissions.Add(new OperationPermission()
                        {
                            OperationId = operation.Id,
                            PermissionId = permission.Id
                        });
                    }
                   
                }
            }
            _repoManager.OperationPermission.AddRange(newOperationPermissions);

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

    private async Task<List<Permission>> _CreateOrUpdatePermissionAsync(List<PermissionRequest> requests)
    {
        var newRequests = new List<PermissionRequest>(requests);
        var existingPermissions = await _repoManager.Permission.FindAll().ToListAsync();

        //Case delete
        var permissionNeedToDelete = existingPermissions.Where(x => !newRequests.Select(y => y.Code).Distinct().Contains(x.Code));
        _repoManager.Permission.RemoveRange(permissionNeedToDelete);
        existingPermissions.RemoveAll(x => permissionNeedToDelete.Select(y => y.Code).Distinct().Contains(x.Code));

        //Case update
        foreach (var existingPerrmission in existingPermissions)
        {
            var request = newRequests.FirstOrDefault(x => x.Code == existingPerrmission.Code);
            if (request == null) continue;

            _mapper.Map<PermissionRequest, Permission>(request, existingPerrmission);
        }

        //Case create
        newRequests.RemoveAll(x => existingPermissions.Select(y => y.Code).Contains(x.Code));
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

    private async Task _RemoveCurrentSecurityAsync()
    {
        await _repoManager.TruncateAsync(nameof(MyIdentityDbContext.AccessRules));
        await _repoManager.TruncateAsync(nameof(MyIdentityDbContext.OperationPermissions));
        await _repoManager.TruncateAsync(nameof(MyIdentityDbContext.RolePermissions));
        await _repoManager.TruncateAsync(nameof(MyIdentityDbContext.Operations));
        await _repoManager.TruncateAsync(nameof(MyIdentityDbContext.Permissions));
        await _repoManager.TruncateAsync(nameof(MyIdentityDbContext.Roles));
    }
}

