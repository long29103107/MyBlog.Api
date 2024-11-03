using Authorization.Constants;
using Azure.Core;
using Contracts.Domain.Exceptions.Abtractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using MyBlog.Identity.Domain.Entities;
using MyBlog.Identity.Service.Abstractions;
using Newtonsoft.Json;
using Shared.Dtos.Identity.Authenticate;
using Shared.Dtos.Identity.Token;
using System.Security.Claims;
using static MyBlog.Identity.Domain.Exceptions.UserException;

namespace MyBlog.Identity.Service.Implements;

public class AuthenticateService : IAuthenticateService
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public AuthenticateService(UserManager<User> userManager, ITokenService tokenService, RoleManager<Role> roleManager)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _roleManager = roleManager;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            throw new NotFoundUserEmail(request.Email);
        }

        if(!await _userManager.CheckPasswordAsync(user, request.Password))
        {
            throw new BadRequestException("Login failed !");
        }

        if (!user.EmailConfirmed)
        {
            throw new BadRequestException("Please check your email and activate this account!");
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
        var userRoles = await _userManager.GetRolesAsync(user);

        var rolesIds = await _roleManager.Roles
            .Where(x => userRoles.Contains(x.Name))
            .Select(x => x.Id)
            .Distinct()
            .ToListAsync();

        var result = new List<Claim>
        {
            new Claim(ClaimConstants.RoleIds, JsonConvert.SerializeObject(rolesIds)),
            new Claim(ClaimConstants.UserId, user.Id.ToString()),
            new Claim(ClaimConstants.Email, user.Email),
        };

        return result;
    }
}