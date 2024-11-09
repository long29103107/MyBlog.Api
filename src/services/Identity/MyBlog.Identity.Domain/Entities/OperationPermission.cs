using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.Identity.Domain.Entities;

public class OperationPermission
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int OperationId { get; set; }
    public Operation Operation { get; set; }

    public int PermissionId { get; set; }
    public Permission Permission { get; set; }
}

