using Contracts.Dtos;

namespace Shared.Dtos.Identity;

public static class SeedDtos
{
    public sealed class OperationRequest
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsLocked { get; set; } = true;
    }

    public sealed class PermissionRequest
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsLocked { get; set; } = true;
        public List<string> Children { get; set; } = new();
    }

    public sealed class RoleRequest
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsLocked { get; set; } = true;
    }
    public sealed class SeedDataRequest : Request
    {
        public bool IsReset { get; set; } = false;
        public bool IsSeed { get; set; } = false;
    }

    public sealed class PermissionIncludeCodeResponse
    {
        public int? OperationId  { get; set; }
        public int? ScopeId { get; set; }
        public string Code { get; set; }
    }
}

