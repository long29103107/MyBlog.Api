namespace Shared.Dtos.Identity.Seed;

public class OperationRequest
{
    public string Code { get; set; }
    public string Name { get; set; }
    public bool IsLocked { get; set; } = true;
}

