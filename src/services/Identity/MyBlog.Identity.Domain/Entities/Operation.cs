using Contracts.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlog.Identity.Domain.Entities;

public class Operation : AuditEntity<int>
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public bool IsLocked { get; set; }
    public bool Mode { get; set; } = false;
    public ICollection<OperationPermission> OperationPermissions { get; set; }
}

    