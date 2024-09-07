//using Contracts.Abstractions.Shared;
//using Microsoft.AspNetCore.Mvc;
//using MyBlog.Post.Service.Abstractions;
//using MyBlog.Shared.Lib;
//using static Shared.Dtos.Category.CategoryDtos;
//using static Shared.Dtos.Post.PostDtos;

//namespace MyBlog.Post.Api.Controllers;


//[Route("api/[controller]")]
//[ApiController]
//public partial class CategoriesController : ControllerBase
//{
//    private readonly ICategoryService _service;

//    public CategoriesController(ICategoryService service)
//    {
//        _service = service;
//    }

//    [HttpGet]
//    public async Task<IActionResult> GetListAsync([FromQuery] CategoryListRequest request)
//    {
//        return Ok(await _service.GetListAsync(request));
//    }

//    [HttpGet("{id}")]
//    public async Task<IActionResult> GetAsync([FromRoute] int id)
//    {
//        return Ok(await _service.GetAsync(id));
//    }

//    [HttpPost]
//    public async Task<IActionResult> CreateAsync([FromBody] CategoryCreateRequest request)
//    {
//        return Ok(await _service.CreateAsync(request));
//    }

//    [HttpPut("{id}")]
//    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] CategoryUpdateRequest request)
//    {
//        return Ok(await _service.UpdateAsync(id, request));
//    }

//    [HttpDelete("{id}")]
//    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
//    {
//        await _service.DeleteAsync(id);
//        return Ok();
//    }

//    [HttpPost("{categoryId}/posts/{postId}")]
//    public async Task<IActionResult> AddPostToCategoryAsync([FromRoute] int categoryId, [FromRoute] int postId)
//    {
//        await _service.AddPostToCategoryAsync(categoryId, postId);
//        return Ok();
//    }


//    [HttpDelete("{categoryId}/posts/{postId}")]
//    public async Task<IActionResult> RemovePostFromCategoryAsync([FromRoute] int categoryId, [FromRoute] int postId)
//    {
//        await _service.RemovePostFromCategoryAsync(categoryId, postId);
//        return Ok();
//    }
//}
