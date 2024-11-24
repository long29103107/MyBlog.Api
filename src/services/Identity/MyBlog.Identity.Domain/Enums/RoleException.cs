using Contracts.Domain.Exceptions.Abtractions;

namespace MyBlog.Identity.Domain.Exceptions;

public static class RoleException
{
    public class ExistingRole : BadRequestException
    {
        public ExistingRole(string code, string name) 
            : base($"The role with code `{code}` and name {name} is existing in app.") { }
    }

    public class NotFound : NotFoundException
    {
        public NotFound(int id) : base($"The role `{id}` was not found.") { }
    }
}
