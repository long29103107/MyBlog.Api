using Contracts.Domain.Exceptions.Abtractions;

namespace MyBlog.Identity.Domain.Exceptions;

public static class PermissionException
{
    public class NotFound : NotFoundException
    {
        public NotFound(int id) : base($"The perrmission `{id}` was not found.") { }
    }
}
