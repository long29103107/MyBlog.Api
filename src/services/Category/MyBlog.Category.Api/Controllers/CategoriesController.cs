﻿using Microsoft.AspNetCore.Mvc;
using MyBlog.Category.Service.Interfaces;

namespace MyBlog.Category.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _service;
    public CategoriesController(ICategoryService service)
    {
        _service = service;

    }

    [HttpPost("seed")]
    public async Task<IActionResult> SeedDataAsync()
    {
        await _service.SeedDataAsync();
        return Ok("Seeding data successfully");
    }

    [HttpGet]
    public async Task<IActionResult> GetListAsync()
    {
        return Ok(await _service.GetListAsync());
    }
}
