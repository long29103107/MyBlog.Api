using static Shared.Dtos.Identity.SeedDtos;

namespace MyBlog.Identity.Service.Abstractions;

public interface ISeedService : IBaseIdentityService
{
    Task SeedDataAsync(SeedDataRequest request);
    Task SeedAccountAsync();
}
