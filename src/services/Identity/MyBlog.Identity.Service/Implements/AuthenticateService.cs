using Azure.Core;
using Contracts.Domain.Exceptions.Abtractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using MyBlog.Identity.Domain.Entities;
using MyBlog.Identity.Service.Abstractions;
using Shared.Dtos.Identity.Authenticate;
using Shared.Dtos.Identity.Token;
using System.Security.Claims;
using static MyBlog.Identity.Domain.Exceptions.UserException;

namespace MyBlog.Identity.Service.Implements;

public class AuthenticateService : IAuthenticateService
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<User> userManager;

    public AuthenticateService(UserManager<User> userManager, ITokenService tokenService)
    {
        this.userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            throw new NotFoundUserEmail(request.Email);
        }

        if(!await userManager.CheckPasswordAsync(user, request.Password))
        {
            throw new BadRequestException("Login failed !");
        }

        var authClaims = await _GetClaimsAsync(user);

        var tokenResponse = _tokenService.GetToken(new TokenRequest()
        {
            Claims = authClaims
        });

        return new LoginResponse()
        {
            AccessToken = tokenResponse.AccessToken,
            ExpiredDate = tokenResponse.ExpiredDate
        };
    }

    private async Task<List<Claim>> _GetClaimsAsync(User user)
    {
        var userRoles = await userManager.GetRolesAsync(user);

        var result = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        foreach (var userRole in userRoles)
        {
            result.Add(new Claim(ClaimTypes.Role, userRole));
        }

        return result;
    }
}

