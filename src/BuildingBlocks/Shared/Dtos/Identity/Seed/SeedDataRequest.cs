using Contracts.Dtos;

namespace Shared.Dtos.Identity.Seed;

public sealed class SeedDataRequest : Request
{
    public bool IsReset { get; set; } = false;
    public bool IsSeed { get; set; } = false;
}

