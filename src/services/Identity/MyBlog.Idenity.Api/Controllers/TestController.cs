using Authorization.Attributes;
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
    [LonGAuth()]
    public async Task<IActionResult> GetAsync()
    {
        return GetResponse(await _userService.GetUserIdsAsync());
    }
}
