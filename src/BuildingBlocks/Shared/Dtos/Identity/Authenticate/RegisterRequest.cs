using Contracts.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos.Identity.Authenticate;

public sealed class RegisterRequest : Request
{
    [Required(ErrorMessage = "User Name is required")]
    public string Username { get; set; }

    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}

