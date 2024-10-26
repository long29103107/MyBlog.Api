using Contracts.Dtos;

namespace Shared.Dtos.Identity.Token;

public sealed class TokenResponse : Response
{
    public string AccessToken { get; set; }
    public DateTime ExpiredDate { get; set; }
}

