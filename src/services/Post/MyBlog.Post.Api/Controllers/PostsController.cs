using Authorization.Attributes;
using Authorization.Constants;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Post.Service.Abstractions;
using MyBlog.Shared.Lib;
using Shared.APIs;
using static Shared.Dtos.Post.PostDtos;

namespace MyBlog.Post.Api.Controllers;

public partial class PostsController : CustomControllerBase
{
    private readonly IPostService _service;

    public PostsController(IPostService service)
    {
        _service = service;
    }

    [HttpGet]
    [LonGAuth(ScopeCodeContants.Post, OperationCodeContants.Read)]
    public async Task<IActionResult> GetListAsync([FromQuery] PostListRequest request)
    {
        return GetResponse(await _service.GetListAsync(request));
    }

    [HttpGet("paging-list")]
    public async Task<IActionResult> GetPagedListAsync([FromQuery] PagingPostRequest request)
    {
        return GetResponse(request.GetListResponse(await _service.GetPagedListAsync(request)));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute] int id)
    {
        return GetResponse(await _service.GetAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] PostCreateRequest request)
    {
        return GetResponse(await _service.CreateAsync(request));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] PostUpdateRequest request)
    {
        return GetResponse(await _service.UpdateAsync(id, request));
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdatePartialAsync([FromRoute] int id
        , [FromBody] JsonPathRequest<PostUpdatePartialRequest> request)
    {
        return GetResponse(await _service.UpdatePartialAsync(id, request));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        return GetResponse(await _service.DeleteAsync(id));
    }

    [HttpPost("{id}/images")]
    public async Task<IActionResult> AddImageToPostAsync([FromRoute] int id)
    {
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
