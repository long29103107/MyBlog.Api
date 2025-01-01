using Microsoft.AspNetCore.Mvc;
using MyBlog.Identity.Service.Abstractions;
using Shared.APIs;
using static Shared.Dtos.Identity.Permission.PermissionDtos;

namespace MyBlog.Identity.Api.Controllers;

public class PermissionsController : CustomControllerBase
{
    private readonly IPermissionService _service;

    public PermissionsController(IPermissionService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetListAsync([FromQuery] PermissionListByRoleRequest request)
    {
        return GetResponse(await _service.GetPermissionByRoleIdAsync(request));
    }
}
