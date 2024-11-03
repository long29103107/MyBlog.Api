using Contracts.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos.Identity.Authenticate;

public sealed class LoginResponse : Response
{
    public string AccessToken { get; set; }
    public DateTime ExpiredDate { get; set; }
}

