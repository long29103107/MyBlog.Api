using Shared.Dtos.Identity.Token;

namespace MyBlog.Identity.Service.Abstractions;

public interface ITokenService
{
    TokenResponse GetToken(TokenRequest request);
}
