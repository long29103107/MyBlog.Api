using AutoMapper;
using Infrastructures.Common;
using Microsoft.IdentityModel.Tokens;
using MyBlog.Identity.Repository.Abstractions;
using MyBlog.Identity.Service.Abstractions;
using MyBlog.Identity.Service.DependencyInjection.Options;
using Shared.Dtos.Identity.Token;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MyBlog.Identity.Service.Implements;

public class TokenService : BaseService<IRepositoryManager>, ITokenService
{
    private readonly JWT _jwtSettings;

    public TokenService(IRepositoryManager repoManager, IMapper mapper, JWT jwtSettings) 
        : base(repoManager, mapper)
    {
        _jwtSettings = jwtSettings;
    }

    public TokenResponse GetToken(TokenRequest request)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.ValidIssuer,
            audience: _jwtSettings.ValidAudience,
            expires: DateTime.Now.AddHours(3),
            claims: request.Claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return new TokenResponse
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiredDate = token.ValidTo
        };
    }
}

