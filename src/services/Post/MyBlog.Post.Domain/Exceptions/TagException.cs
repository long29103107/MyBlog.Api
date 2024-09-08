using Contracts.Domain.Exceptions.Abtractions;
using MyBlog.Post.Domain.Entities;

namespace MyBlog.Post.Domain.Exceptions;

public static class TagException
{
    public class NotFound : NotFoundException
    {
        public NotFound(string name) : base(
            string.IsNullOrEmpty(name)
                ? "The tag name must be required" 
                : $"The tag {name} was not found.")
        { }

        public NotFound(int id) : base($"The tag with the id {id} was not found.")
        { }
    }
}
