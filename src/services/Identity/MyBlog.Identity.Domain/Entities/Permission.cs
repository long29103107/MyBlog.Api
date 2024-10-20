using Contracts.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlog.Identity.Domain.Entities;

public class Permission : AuditEntity<int>
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    [ForeignKey("Role")]
    public int? RoleId { get; set; }
    public Role Role { get; set; }
}

