using Contracts.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlog.Identity.Domain.Entities;

public class AccessRule : AuditEntity<int>
{
    [ForeignKey("Permission")]
    public int? PermissionId { get; set; }
    public Permission Permission { get; set; }

    [ForeignKey("Role")]
    public int? RoleId { get; set; }
    public Role Role { get; set; }

    public bool Mode { get; set; } = false;
}
