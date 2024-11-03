using Shared.Dtos.Identity.Seed;

namespace MyBlog.Identity.Service.Abstractions;

public interface ISeedService : IBaseIdentityService
{
    Task SeedDataAsync(SeedDataRequest request);
}
