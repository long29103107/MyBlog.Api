//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using MyBlog.Identity.Domain.Entities;
//using MyBlog.Identity.Repository.Abstractions;

//namespace MyBlog.Identity.Repository.Stores;

//public class RoleStore : IRoleStore<Role>
//{
//    private readonly IRoleRepository _roleRepository;

//    public RoleStore(IRoleRepository roleRepository)
//    {
//        _roleRepository = roleRepository;
//    }

//    public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        _roleRepository.Add(role);
//        await _roleRepository.SaveAsync();

//        return IdentityResult.Success;
//    }

//    public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        _roleRepository.Update(role);
//        await _roleRepository.SaveAsync();

//        return IdentityResult.Success;
//    }

//    public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        _roleRepository.Remove(role);
//        await _roleRepository.SaveAsync();

//        return IdentityResult.Success;
//    }

//    public async Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        int id = 0;
//        if (!int.TryParse(roleId, out id))
//        {
//            throw new InvalidCastException($"Cannot format the data from the string value to the int value");
//        }

//        var result = await _roleRepository.FirstOrDefaultAsync(x => x.Id == id);

//        return result;
//    }

//    public async Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
//    {
//        cancellationToken.ThrowIfCancellationRequested();

//        var result = await _roleRepository
//            .FindByCondition(x => x.NormalizedName.Equals(normalizedRoleName))
//            .FirstOrDefaultAsync();

//        return result;
//    }

//    public async Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
//    {
//        return role.Id.ToString();
//    }

//    public async Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
//    {
//        return role.Name;
//    }

//    public async Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
//    {
//        role.Name = roleName;
//    }

//    public async Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
//    {
//        return role.NormalizedName!;
//    }

//    public async Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken)
//    {
//        role.NormalizedName = normalizedName;
//    }

//    public void Dispose()
//    {
//        _roleRepository.Dispose();
//    }
//}

