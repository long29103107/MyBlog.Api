using Microsoft.AspNetCore.Identity;

namespace MyBlog.Identity.Domain.Entities;

public class UserRole : IdentityUserRole<int>
{
    public virtual User User { get; set; }
    public virtual Role Role { get; set; }
}

