using Microsoft.AspNetCore.Mvc;
using MyBlog.Identity.Api.Controllers;
using MyBlog.Identity.Service.Abstractions;
using Shared.APIs;
using Shared.Dtos.Identity.Authenticate;

namespace MyBlog.Idenity.Api.Controllers;

public class LoginController : CustomControllerBase
{
    private readonly IAuthenticateService _authenticateService;

    public LoginController(IAuthenticateService authenticateService)
    {
        _authenticateService = authenticateService;
    }

    [HttpPost]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
    {
        return GetResponse(await _authenticateService.LoginAsync(request));
    }
}
