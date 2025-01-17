using Microsoft.AspNetCore.Mvc;
using MyBlog.Identity.Service.Abstractions;
using Shared.APIs;
using Shared.Dtos.Identity.Register;

namespace MyBlog.Idenity.Api.Controllers;

public class RegisterController : CustomControllerBase
{
    private readonly IRegisterService _registerService;

    public RegisterController(IRegisterService registerService)
    {
        _registerService = registerService;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        return GetResponse(await _registerService.RegisterAsync(request));
    }
}
