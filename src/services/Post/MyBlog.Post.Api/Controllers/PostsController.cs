using Contracts.Abstractions.Shared;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Post.Domain.Entities;
using MyBlog.Post.Service.Abstractions;
using MyBlog.Shared.Lib;
using static Shared.Dtos.Post.PostDtos;

namespace MyBlog.Post.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public partial class PostsController : ControllerBase
{
    //POST /posts: Tạo một bài viết mới.
    //GET /posts/{id}: Lấy thông tin chi tiết về một bài viết.
    //PUT /posts/{id}: Cập nhật thông tin một bài viết.
    //DELETE /posts/{id}: Xóa một bài viết.
    //POST /posts/{id}/ images: Thêm hình ảnh vào bài viết.
    //POST /posts/{id}/ comments: Thêm bình luận vào bài viết.
    //POST /posts/{id}/ tags: Thêm tag vào bài viết.
    //DELETE /posts/{id}/ tags /{ tagName}: Xóa một tag khỏi bài viết.

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
}
