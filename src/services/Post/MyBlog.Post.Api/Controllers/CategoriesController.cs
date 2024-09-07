using Contracts.Abstractions.Shared;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Post.Service.Abstractions;
using MyBlog.Shared.Lib;
using static Shared.Dtos.Category.CategoryDtos;
using static Shared.Dtos.Post.PostDtos;

namespace MyBlog.Post.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public partial class CategoriesController : ControllerBase
{
    //POST /categories: Tạo một danh mục mới.
    //GET /categories/{id}: Lấy thông tin chi tiết về một danh mục.
    //PUT /categories/{id}: Cập nhật thông tin một danh mục.
    //DELETE /categories/{id}: Xóa một danh mục.
    //POST /categories/{id}/ posts: Thêm một bài viết vào danh mục.
    //DELETE /categories/{id}/ posts /{ postId}: Xóa một bài viết khỏi danh mục.

    private readonly ICategoryService _service;

    public CategoriesController(ICategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetListAsync([FromQuery] CategoryListRequest request)
    {
        return Ok(await _service.GetListAsync(request));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute] int id)
    {
        return Ok(await _service.GetAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CategoryCreateRequest request)
    {
        return Ok(await _service.CreateAsync(request));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] CategoryUpdateRequest request)
    {
        return Ok(await _service.UpdateAsync(id, request));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost("{categoryId}/posts/{postId}")]
    public async Task<IActionResult> AddPostToCategoryAsync([FromRoute] int categoryId, [FromRoute] int postId)
    {
        await _service.AddPostToCategoryAsync(categoryId, postId);
        return NoContent();
    }


    [HttpDelete("{categoryId}/posts/{postId}")]
    public async Task<IActionResult> RemovePostFromCategoryAsync([FromRoute] int categoryId, [FromRoute] int postId)
    {
        await _service.RemovePostFromCategoryAsync(categoryId, postId);
        return NoContent();
    }
}
