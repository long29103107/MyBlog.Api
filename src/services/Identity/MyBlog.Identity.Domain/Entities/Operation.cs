using Contracts.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlog.Identity.Domain.Entities;

public class Operation : AuditEntity<int>
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    [ForeignKey("Permission")]
    public int? PermissionId { get; set; }
    public Permission Permission { get; set; }

}

    