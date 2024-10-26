using Contracts.Dtos;
using System.Security.Claims;

namespace Shared.Dtos.Identity.Token;

public sealed class TokenRequest : Request
{
    public List<Claim> Claims { get; set; } = new List<Claim>();
}

