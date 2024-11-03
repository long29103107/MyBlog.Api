using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyBlog.Idenity.Api.Authentication;
using MyBlog.Identity.Api.Controllers;
using MyBlog.Identity.Domain.Entities;
using MyBlog.Identity.Service.Abstractions;
using Shared.Dtos.Identity.Authenticate;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyBlog.Idenity.Api.Controllers;

public class AuthenticateController : CustomIdentityControllerBase
{
    private readonly IAuthenticateService _authenticateService;

    public AuthenticateController(IAuthenticateService authenticateService)
    {
        _authenticateService = authenticateService;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
    {
        return GetResponse(await _authenticateService.LoginAsync(request));
    }

    //[HttpPost]
    //[Route("register")]
    //public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    //{
    //    var userExists = await userManager.FindByNameAsync(request.Username);

    //    if (userExists != null)
    //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

    //    User user = new User()
    //    {
    //        Email = request.Email,
    //        SecurityStamp = Guid.NewGuid().ToString(),
    //        UserName = request.Username
    //    };
    //    var result = await userManager.CreateAsync(user, request.Password);
    //    if (!result.Succeeded)
    //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

    //    return Ok(new Response { Status = "Success", Message = "User created successfully!" });
    //}
}
