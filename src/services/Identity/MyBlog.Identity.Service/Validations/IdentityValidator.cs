using FluentValidation;
using MyBlog.Identity.Domain.Entities;
using MyBlog.Identity.Repository.Abstractions;

namespace MyBlog.Identity.Service.Validations;

public class IdentityValidator : AbstractValidator<Role>
{
    private IRepositoryManager _repoManager;
    public IdentityValidator(IRepositoryManager repoManager)
    {
        _repoManager = repoManager;
        RuleFor(p => p.Code).NotEmpty();
        RuleFor(p => p.Name).NotEmpty();
        //RuleFor(ExistingRoleAsync);
    }

    private async Task ExistingRoleAsync()
    {

    }
}