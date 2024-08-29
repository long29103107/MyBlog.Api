using Microsoft.AspNetCore.Mvc;

namespace MyBlog.Contracts;

public class CustomProblemDetails : ProblemDetails
{
    public IReadOnlyCollection<ValidationError> Errors { get; set; }
}