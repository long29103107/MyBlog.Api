namespace MyBlog.Contracts.Dtos.Interfaces;

public interface IRequest
{
    IScopedContext? ScopedContext { get; set; }
}

