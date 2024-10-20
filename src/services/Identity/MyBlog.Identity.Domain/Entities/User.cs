using Contracts.Domains.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace MyBlog.Identity.Domain.Entities;
public class User : IdentityUser<int>, IDateTracking, IUserTracking
{
    public string CreatedBy { get; set; } = string.Empty!;
    public string UpdatedBy { get; set; } = string.Empty!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
}
