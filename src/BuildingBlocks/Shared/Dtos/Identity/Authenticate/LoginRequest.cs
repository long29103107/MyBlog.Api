using Contracts.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos.Identity.Authenticate;

public sealed class LoginRequest : Request
{
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}

