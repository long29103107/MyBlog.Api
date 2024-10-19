using Microsoft.AspNetCore.Identity;

namespace MyBlog.Identity.Domain.Entities;

public class Role : IdentityRole
{
    public string Code { get; set; }
}

