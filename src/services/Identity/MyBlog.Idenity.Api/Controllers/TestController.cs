using Microsoft.AspNetCore.Mvc;
using MyBlog.Identity.Service.Abstractions;
using Shared.APIs;

namespace MyBlog.Idenity.Api.Controllers;

public class TestController : CustomControllerBase
{
    private readonly IUserService _userService;

    public TestController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetListAsync()
    { 
        return Ok(await _userService.GetUserIdsAsync());
    }
}
