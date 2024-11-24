using Microsoft.AspNetCore.Mvc;
using MyBlog.Identity.Service.Abstractions;
using static Shared.Dtos.Identity.Role.RoleDtos;
namespace MyBlog.Identity.Api.Controllers;

public class RolesController : CustomIdentityControllerBase
{
    private readonly IRoleService _service;

    public RolesController(IRoleService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetListAsync([FromQuery] RoleListRequest request)
    {
        return GetResponse(await _service.GetListAsync(request));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute] int id)
    {
        return GetResponse(await _service.GetAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] RoleCreateRequest request)
    {
        return GetResponse(await _service.CreateAsync(request));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] RoleUpdateRequest request)
    {
        return GetResponse(await _service.UpdateAsync(id, request));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        return GetResponse(await _service.DeleteAsync(id));
    }
}
