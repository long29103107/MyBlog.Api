using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBlog.Identity.Domain.Entities;
using MyBlog.Identity.Repository.Abstractions;
using MyBlog.Identity.Repository.Implements;
using StackExchange.Redis;

namespace MyBlog.Identity.Repository.Stores;

public class UserStore : IUserStore<User>, IUserPasswordStore<User>
{
    private readonly IUserRepository _userRepository;

    public UserStore(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _userRepository.Add(user);
        await _userRepository.SaveAsync();

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _userRepository.Remove(user);
        await _userRepository.SaveAsync();

        return IdentityResult.Success;
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public async Task<User?> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        int id = 0;
        if (!int.TryParse(userId, out id))
        {
            throw new InvalidCastException($"Cannot format the data from the string value to the int value");
        }

        var result = await _userRepository.FirstOrDefaultAsync(x => x.Id == id);

        return result;
    }

    public async Task<User?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        var result = await _userRepository.FirstOrDefaultAsync(x => x.NormalizedUserName == normalizedUserName);

        return result;
    }

    public async Task<string?> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
    {
        return user.NormalizedUserName;
    }

    public async Task<string?> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
    {
        return user.PasswordHash;
    }

    public async Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
    {
        return user.Id.ToString();
    }

    public async Task<string?> GetUserNameAsync(User user, CancellationToken cancellationToken)
    {
        return user.UserName;
    }

    public async Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
    {
        return user.PasswordHash != null;
    }

    public async Task SetNormalizedUserNameAsync(User user, string? normalizedName, CancellationToken cancellationToken)
    {
        user.NormalizedUserName = normalizedName;
    }

    public async Task SetPasswordHashAsync(User user, string? passwordHash, CancellationToken cancellationToken)
    {
        user.PasswordHash = passwordHash;
    }

    public async Task SetUserNameAsync(User user, string? userName, CancellationToken cancellationToken)
    {
        user.UserName = userName;
    }

    public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _userRepository.Update(user);
        await _userRepository.SaveAsync();

        return IdentityResult.Success;
    }
}

