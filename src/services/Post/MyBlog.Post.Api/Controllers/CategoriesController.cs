using Microsoft.AspNetCore.Mvc;
using MyBlog.Post.Service.Abstractions;
using Shared.APIs;
using static Shared.Dtos.Category.CategoryDtos;

namespace MyBlog.Post.Api.Controllers;

public partial class CategoriesController : CustomControllerBase
{
    private readonly ICategoryService _service;

    public CategoriesController(ICategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetListAsync([FromQuery] CategoryListRequest request)
    {
        return GetResponse(await _service.GetListAsync(request));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute] int id)
    {
        return GetResponse(await _service.GetAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CategoryCreateRequest request)
    {
        return GetResponse(await _service.CreateAsync(request));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] CategoryUpdateRequest request)
    {
        return GetResponse(await _service.UpdateAsync(id, request));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        await _service.DeleteAsync(id);
        return GetResponse(StatusCodes.Status200OK);
    }

    [HttpPost("{categoryId}/posts/{postId}")]
    public async Task<IActionResult> AddPostToCategoryAsync([FromRoute] int categoryId, [FromRoute] int postId)
    {
        await _service.AddPostToCategoryAsync(categoryId, postId);
        return GetResponse(StatusCodes.Status200OK);
    }


    [HttpDelete("{categoryId}/posts/{postId}")]
    public async Task<IActionResult> RemovePostFromCategoryAsync([FromRoute] int categoryId, [FromRoute] int postId)
    {
        await _service.RemovePostFromCategoryAsync(categoryId, postId);
        return GetResponse(StatusCodes.Status200OK);
    }
}
