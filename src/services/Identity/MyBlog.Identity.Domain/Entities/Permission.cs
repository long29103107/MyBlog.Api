using Contracts.Domain;

namespace MyBlog.Identity.Domain.Entities;

public class Permission : AuditEntity<int>
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
}

