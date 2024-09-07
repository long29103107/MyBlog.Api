using Contracts.Abstractions.Shared;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Post.Service.Abstractions;
using MyBlog.Shared.Lib;
using static Shared.Dtos.Post.PostDtos;

namespace MyBlog.Post.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public partial class PostsController : ControllerBase
{
    private readonly IPostService _service;

    public PostsController(IPostService service)
    {
        _service = service;
    }

    //[HttpPost("seed")]
    //public async Task<IActionResult> SeedDataAsync()
    //{
    //    await _service.SeedDataAsync();
    //    return Ok("Seeding data successfully");
    //}

    [HttpGet]
    public async Task<IActionResult> GetListAsync(PostListRequest request)
    {
        return Ok(await _service.GetListAsync(request));
    }

    [HttpGet("paging-list")]
    public async Task<IActionResult> GetPagedListAsync(PagingRequest request)
    {
        return Ok(await _service.GetPagedListAsync(request));
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

    [HttpPost("{id}/images")]
    public async Task<IActionResult> AddImageToPostAsync([FromRoute] int id)
    {
        //await _service.DeleteAsync(id);
        return Ok();
    }

    [HttpPost("{id}/comments")]
    public async Task<IActionResult> AddCommentToPostAsync([FromRoute] int id)
    {
        return Ok();
    }

    [HttpPost("{id}/tags")]
    public async Task<IActionResult> AddTagToPostAsync([FromRoute] int id)
    {
        return Ok();
    }

    [HttpDelete("{id}/tags")]
    public async Task<IActionResult> RemoveTagFromPostAsync([FromRoute] int id)
    {
        return Ok();
    }
}
