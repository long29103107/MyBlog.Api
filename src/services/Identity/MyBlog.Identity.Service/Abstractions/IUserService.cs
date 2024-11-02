namespace MyBlog.Identity.Service.Abstractions;

public interface IUserService : IBaseIdentityService
{
    Task<IEnumerable<int>> GetUserIdsAsync();
}
