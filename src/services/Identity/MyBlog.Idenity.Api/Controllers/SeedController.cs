﻿using Microsoft.AspNetCore.Mvc;
using MyBlog.Identity.Service.Abstractions;
using Shared.APIs;
using static Shared.Dtos.Identity.SeedDtos;

namespace MyBlog.Identity.Api.Controllers;

public class SeedController : CustomControllerBase
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

    [HttpPost("account")]
    public async Task<IActionResult> SeedAccountSuperAdmin()
    {
        await _seedService.SeedAccountAsync();
        return GetResponse();
    }
}
