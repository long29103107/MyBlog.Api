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
using static Shared.Dtos.Identity.UserDtos;

namespace MyBlog.Identity.Service.Implements;

public class UserService : BaseIdentityService, IUserService
{
    public UserService(IRepositoryManager repoManager, IMapper mapper, IValidatorFactory validatorFactory, ILogger logger )
        : base(repoManager, mapper, validatorFactory, logger)
    {
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

    private async Task<User> _GetActiveUserAsync(int id)
    {
        return await _repoManager.User.FirstOrDefaultAsync(x => x.Id == id);
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

