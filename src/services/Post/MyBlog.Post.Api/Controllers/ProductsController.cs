using Microsoft.AspNetCore.Mvc;
using MyBlog.Post.Service.Interfaces;
using MyBlog.Contracts.Domains.ValueOf;

namespace MyBlog.Post.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IPostService _service;

    public ProductsController(IPostService service)
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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromQuery] int id)
    {
        return Ok(await _service.GetAsync(id));
    }
}
