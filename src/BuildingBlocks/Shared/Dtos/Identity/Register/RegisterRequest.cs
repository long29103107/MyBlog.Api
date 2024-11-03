using Contracts.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos.Identity.Register;

public sealed class RegisterRequest : Request
{
    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}

