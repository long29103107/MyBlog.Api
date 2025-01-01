using Contracts.Abstractions.Common;
using MyBlog.Identity.Repository.Abstractions;
using Shared.Dtos.Identity.Token;

namespace MyBlog.Identity.Service.Abstractions;

public interface ITokenService : IBaseService<IRepositoryManager>
{
    TokenResponse GetToken(TokenRequest request);
}
