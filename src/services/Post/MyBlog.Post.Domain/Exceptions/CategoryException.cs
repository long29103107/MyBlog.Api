using Contracts.Domain.Exceptions.Abtractions;

namespace MyBlog.Post.Domain.Exceptions;

public static class CategoryException
{
    public class NotFound : NotFoundException
    {
        public NotFound(int categoryId) : base($"The category with the id {categoryId} was not found.") { }
    }
}
