namespace Shared.Dtos.Identity.Seed;

public class RoleRequest
{
    public string Code { get; set; }
    public string Name { get; set; }
    public bool IsLocked { get; set; } = true;
}

