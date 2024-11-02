using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Identity.Service.Abstractions;

namespace MyBlog.Identity.Api.Controllers;

public class TestController : CustomIdentityControllerBase
{
    private readonly IUserService _userService;

    public TestController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAsync()
    {
        var result = await _userService.GetUserIdsAsync();
        return Ok(result);
    }
}
