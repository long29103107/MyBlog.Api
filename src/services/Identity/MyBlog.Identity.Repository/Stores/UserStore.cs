//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using MyBlog.Identity.Domain.Entities;
//using MyBlog.Identity.Repository.Abstractions;

//namespace MyBlog.Identity.Repository.Stores;

//public class UserStore : IUserStore<User>, IUserPasswordStore<User>, IUserRoleStore<User>
//{
//    private readonly IRepositoryManager _repoManager;

//    public UserStore(IRepositoryManager repoManager)
//    {
//        _repoManager = repoManager;
//    }

//    public async Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
//    {
//        // Check if the role exists
//        var role = await _repoManager.Role.FirstOrDefaultAsync(r => r.Name == roleName)
//            ?? throw new InvalidOperationException($"Role '{roleName}' does not exist."); ;

//        // Check if the user is already in the role
//        var userRole = await _repoManager.UserRoles
//            .SingleOrDefaultAsync(ur => ur.UserId == user.Id && ur.RoleId == role.Id, cancellationToken);

//        if (userRole == null)
//        {
//            // Add a new UserRole relationship
//            _repoManager.UserRoles.Add(new UserRole
//            {
//                UserId = user.Id,
//                RoleId = role.Id
//            });
//            await _repoManager.SaveAsync();
//        }
//    }

//    public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        _repoManager.User.Add(user);
//        await _repoManager.SaveAsync();

//        return IdentityResult.Success;
//    }

//    public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        _repoManager.User.Remove(user);
//        await _repoManager.SaveAsync();

//        return IdentityResult.Success;
//    }

//    public void Dispose()
//    {
//        _repoManager.Dispose();
//    }

//    public async Task<User?> FindByIdAsync(string userId, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        int id = 0;
//        if (!int.TryParse(userId, out id))
//        {
//            throw new InvalidCastException($"Cannot format the data from the string value to the int value");
//        }

//        var result = await _repoManager.User.FirstOrDefaultAsync(x => x.Id == id);

//        return result;
//    }

//    public async Task<User?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
//    {
//        var result = await _repoManager.User.FirstOrDefaultAsync(x => x.NormalizedUserName == normalizedUserName);

//        return result;
//    }

//    public async Task<string?> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
//    {
//        return user.NormalizedUserName;
//    }

//    public async Task<string?> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
//    {
//        return user.PasswordHash;
//    }

//    public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
//    {
//        // Ensure the cancellation token is respected
//        cancellationToken.ThrowIfCancellationRequested();

//        // Query roles associated with the user
//        var roles = await _repoManager.UserRoles
//            .Where(ur => ur.UserId == user.Id)
//            .Select(ur => ur.Role.Name)  // Assuming Role is a navigation property in UserRole
//            .ToListAsync(cancellationToken);

//        return roles;
//    }

//    public async Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
//    {
//        return user.Id.ToString();
//    }

//    public async Task<string?> GetUserNameAsync(User user, CancellationToken cancellationToken)
//    {
//        return user.UserName;
//    }

//    public async Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
//    {
//        // Respect the cancellation token
//        cancellationToken.ThrowIfCancellationRequested();

//        // Find the role by name
//        var role = await _repoManager.Role.FirstOrDefaultAsync(r => r.Name == roleName)
//            ?? throw new InvalidOperationException($"Role '{roleName}' does not exist.");

//        // Find all users in the specified role
//        var usersInRole = await _repoManager.UserRoles
//            .Where(ur => ur.RoleId == role.Id)
//            .Select(ur => ur.User) // Assuming User is a navigation property in UserRole
//            .ToListAsync(cancellationToken);

//        return usersInRole;
//    }

//    public async Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
//    {
//        return user.PasswordHash != null;
//    }

//    public async Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
//    {
//        // Ensure cancellation token is respected
//        cancellationToken.ThrowIfCancellationRequested();

//        // Check if the user is in the role by joining UserRoles and Roles
//        var isInRole = await _repoManager.UserRoles
//            .AnyAsync(ur => ur.UserId == user.Id && ur.Role.Name == roleName);

//        return isInRole;
//    }

//    public async Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
//    {
//        // Ensure the cancellation token is respected
//        cancellationToken.ThrowIfCancellationRequested();

//        // Find the role by name
//        var role = await _repoManager.Role.FirstOrDefaultAsync(r => r.Name == roleName)
//            ?? throw new InvalidOperationException($"Role '{roleName}' does not exist.");

//        // Find the UserRole relationship between the user and the role
//        var userRole = await _repoManager.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == user.Id && ur.RoleId == role.Id);

//        if (userRole is not null)
//        {
//            _repoManager.UserRoles.Remove(userRole);
//            await _repoManager.SaveAsync();
//        }
//    }

//    public async Task SetNormalizedUserNameAsync(User user, string? normalizedName, CancellationToken cancellationToken)
//    {
//        user.NormalizedUserName = normalizedName;
//    }

//    public async Task SetPasswordHashAsync(User user, string? passwordHash, CancellationToken cancellationToken)
//    {
//        user.PasswordHash = passwordHash;
//    }

//    public async Task SetUserNameAsync(User user, string? userName, CancellationToken cancellationToken)
//    {
//        user.UserName = userName;
//    }

//    public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        _repoManager.User.Update(user);
//        await _repoManager.SaveAsync();

//        return IdentityResult.Success;
//    }
//}

