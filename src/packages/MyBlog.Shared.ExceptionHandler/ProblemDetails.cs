using Contracts.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using MyBlog.FluentValidation.Exceptions;

namespace MyBlog.Shared.ExceptionHandler;

public class CustomProblemDetails : ProblemDetails
{
    public IReadOnlyCollection<ValidationError> Errors { get; set; }
}