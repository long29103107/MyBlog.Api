using MyBlog.Contracts;

namespace MyBlog.Post.Domain.Exceptions;

public static class PostException
{
    public class NotFound : NotFoundException
    {
        public NotFound(int productId)
            : base($"The product with the id {productId} was not found.") { }
    }
}
