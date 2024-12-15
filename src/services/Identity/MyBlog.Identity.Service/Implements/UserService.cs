using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contracts.Domain.Constants;
using Contracts.Domain.Exceptions.Abtractions;
using FilteringAndSortingExpression.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBlog.Identity.Domain.Entities;
using MyBlog.Identity.Domain.Exceptions;
using MyBlog.Identity.Repository.Abstractions;
using MyBlog.Identity.Service.Abstractions;
using Serilog;
using static Shared.Dtos.Identity.UserDtos;

namespace MyBlog.Identity.Service.Implements;

public class UserService : BaseIdentityService, IUserService
{
    private readonly RoleManager<Role> _roleManager;
    private readonly UserManager<User> _userManager;

    public UserService(IRepositoryManager repoManager, IMapper mapper, IValidatorFactory validatorFactory, ILogger logger, RoleManager<Role> roleManager, UserManager<User> userManager)
        : base(repoManager, mapper, validatorFactory, logger)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    #region Get
    public async Task<UserResponse> GetAsync(int id)
    {
        var user = await _GetUserAsync(id)
           ?? throw new UserException.NotFound(id);

        return _mapper.Map<UserResponse>(user);
    }
    #endregion

    #region Get Active User
    public async Task<UserResponse> GetActiveAsync(int id)
    {
        var user = await _GetActiveUserAsync(id)
           ?? throw new UserException.NotFound(id);

        return _mapper.Map<UserResponse>(user);
    }
    #endregion

    #region Delete
    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _GetUserAsync(id)
            ?? throw new UserException.NotFound(id);

        _CheckLockUser(user);

        _repoManager.User.Remove(user);
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

    #region Assign role to user
    public async Task AssignRoleAsync(int userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString())
            ?? throw new UserException.NotFound(userId);

        if (!user.IsActive)
        {
            throw new BadRequestException($"User is deactivated");
        }

        var role = await _repoManager.Role.FindByCondition(x => 
                x.Code.Equals(IdentitySchemaConstants.RoleCode.SuperAdmin))
            .FirstOrDefaultAsync()
            ?? throw new RoleException.NameNotFound(IdentitySchemaConstants.RoleCode.SuperAdmin);

        if(!role.IsActive)
        {
            throw new BadRequestException("Role is deactivated");
        }

        _repoManager.User.Detach(user);
        await _userManager.AddToRoleAsync(user, role.Name ?? string.Empty);
    }
    #endregion

    #region Has Permission 
    public async Task<bool> HasPermissionAsync(int userId, string scopeCode, string operationCode)
    {
        var result = false;
        var user = await _GetActiveUserAsync(userId)
           ?? throw new UserException.NotFound(userId);

        //var hasPermission = await (from userRole in _repoManager.UserRole.FindByCondition(x => x.UserId == userId)
                                   
        //                           join role in  _repoManager.Role.FindAll()
        //                           on userRole.RoleId equals role.Id

        //                           join accessRule in _repoManager.AccessRule.FindByCondition(x => x.)


        //                           join )


        return result;
    }
    #endregion

    #region Get List
    public async Task<IEnumerable<UserResponse>> GetListAsync(UserListRequest request)
    {
        var result = await _UserIgnoreGlobalFilter()
           .Filter(request)
           .ProjectTo<UserResponse>(_mapper.ConfigurationProvider)
           .ToListAsync();

        return result;
    }
    #endregion

    #region Update
    public async Task<UserResponse> UpdateAsync(int id, UserUpdateRequest request)
    {
        var user = await _GetUserAsync(id)
          ?? throw new UserException.NotFound(id);

        _CheckLockUser(user);

        var isExistingUser = await _repoManager.User.FindByCondition(x =>
                x.Email.Equals(request.Email)).AnyAsync();

        if (isExistingUser)
        {
            throw new BadRequestException("Existing role has code or name!");
        }

        _mapper.Map<UserUpdateRequest, User>(request, user);
        _repoManager.User.Update(user);
        await _repoManager.SaveAsync();

        return _mapper.Map<UserResponse>(user);
    }
    #endregion

    #region Common Private

    private async Task<User> _GetActiveUserAsync(int id)
    {
        return await _repoManager.User.FirstOrDefaultAsync(x => x.Id == id);
    }

    private async Task<User> _GetUserAsync(int id)
    {
        return await _UserIgnoreGlobalFilter().FirstOrDefaultAsync(x => x.Id == id);
    }

    public IQueryable<User> _UserIgnoreGlobalFilter()
    {
        return _repoManager.User.FindAll().IgnoreQueryFilters();
    }

    public void _CheckLockUser(User user)
    {
        if (user.IsLocked)
        {
            throw new BadRequestException("The user has created from system, cannot update this one!");
        }
    }
    #endregion
}

