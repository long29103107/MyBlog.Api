using Microsoft.AspNetCore.Mvc;
using MyBlog.Identity.Service.Abstractions;
using static Shared.Dtos.Identity.SeedDtos;

namespace MyBlog.Identity.Api.Controllers;

public class SeedController : CustomIdentityControllerBase
{
    private readonly ISeedService _seedService;

    public SeedController(ISeedService seedService)
    {
        _seedService = seedService;
    }

    [HttpPost]
    public async Task<IActionResult> Seed([FromBody] SeedDataRequest request)
    {
        await _seedService.SeedDataAsync(request);
        return GetResponse();
    }
}
