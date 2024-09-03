using FluentValidation;
using Entities = MyBlog.Post.Domain.Entities;

namespace MyBlog.Post.Service.Validations;

public class PostValidator : AbstractValidator<Entities.Post>
{
    public PostValidator()
    {
        RuleFor(p => p.Title).NotEmpty();
        RuleFor(p => p.Content).NotEmpty();
    }
}