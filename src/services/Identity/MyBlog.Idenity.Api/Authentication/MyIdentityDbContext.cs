using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyBlog.Idenity.Api.Authentication
{
    public class MyIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public MyIdentityDbContext(DbContextOptions<MyIdentityDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
