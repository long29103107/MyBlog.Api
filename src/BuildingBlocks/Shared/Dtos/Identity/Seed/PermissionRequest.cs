namespace Shared.Dtos.Identity.Seed;

public class PermissionRequest
{
    public string Code { get; set; }
    public string Name { get; set; }
    public bool IsLocked { get; set; } = true;
    public List<string> Children { get; set; } = new();
}

