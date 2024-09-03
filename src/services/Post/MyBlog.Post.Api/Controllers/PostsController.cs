using Microsoft.AspNetCore.Mvc;
using MyBlog.Post.Service.Abstractions;
using MyBlog.Shared.Lib;
using static Shared.Dtos.Post.PostDtos;

namespace MyBlog.Post.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly IPostService _service;

    public PostsController(IPostService service)
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
    public async Task<IActionResult> GetAsync([FromRoute] int id)
    {
        return Ok(await _service.GetAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] PostCreateRequest request)
    {
        return Ok(await _service.CreateAsync(request));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] PostUpdateRequest request)
    {
        return Ok(await _service.UpdateAsync(id, request));
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdatePartialAsync([FromRoute] int id
        , [FromBody] JsonPathRequest<PostUpdatePartialRequest> request)
    {
        return Ok(await _service.UpdatePartialAsync(id, request));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
