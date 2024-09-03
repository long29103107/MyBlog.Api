
using Contracts.Domain.Exceptions;

namespace MyBlog.Post.Domain.Exceptions;

public static class PostException
{
    public class NotFound : NotFoundException
    {
        public NotFound(int postId) : base($"The post with the id {postId} was not found.") { }
    }
}
