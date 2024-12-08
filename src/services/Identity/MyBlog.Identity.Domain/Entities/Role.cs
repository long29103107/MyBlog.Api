using Microsoft.AspNetCore.Identity;
using Contracts.Domains.Interfaces;

namespace MyBlog.Identity.Domain.Entities;

public class Role : IdentityRole<int>, IDateTracking, IUserTracking
{
    public string CreatedBy { get; set; } = string.Empty!;
    public string UpdatedBy { get; set; } = string.Empty!;
    public string Code { get; set; }
    public int Weight { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsLocked { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public virtual ICollection<AccessRule> RolePermissions { get; set; } = new List<AccessRule>();
}
