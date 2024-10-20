using Contracts.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlog.Identity.Domain.Entities;

public class AccessRule : AuditEntity<int>
{
    [ForeignKey("Operation")]
    public int? OperationId { get; set; }
    public Operation Operation { get; set; }

    [ForeignKey("Permission")]
    public int? PermissionId { get; set; }
    public Permission Permission { get; set; }

    [ForeignKey("Role")]
    public int? RoleId { get; set; }
    public Role Role { get; set; }

}
