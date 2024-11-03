using Contracts.Domain.Exceptions.Abtractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MyBlog.Identity.Domain.Entities;
using MyBlog.Identity.Service.Abstractions;
using Shared.Dtos.Identity.Register;

namespace MyBlog.Identity.Service.Implements;

public class RegisterService : IRegisterService
{
    private readonly UserManager<User> _userManager;

    public RegisterService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
    {
        var userExisting = await _userManager.FindByEmailAsync(request.Email);

        if (userExisting is not null)
            throw new BadRequestException("User already exists!");

        User user = new User()
        {
            Email = request.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            throw new BadRequestException("User creation failed! Please check user details and try again.");
        }

        return new RegisterResponse
        {
            IsRegistered = true
        };
    }
}

