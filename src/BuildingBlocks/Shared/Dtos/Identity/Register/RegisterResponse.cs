using Contracts.Dtos;

namespace Shared.Dtos.Identity.Register;

public sealed class RegisterResponse : Response
{
    public bool IsRegistered { get; set; }
}

