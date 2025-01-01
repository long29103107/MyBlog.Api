using Contracts.Abstractions.Common;
using MyBlog.Identity.Repository.Abstractions;
using static Shared.Dtos.Identity.SeedDtos;

namespace MyBlog.Identity.Service.Abstractions;

public interface ISeedService : IBaseService<IRepositoryManager>
{
    Task SeedDataAsync(SeedDataRequest request);
    Task SeedAccountAsync();
}
