using Microsoft.AspNetCore.Http;
using MyBlog.Contracts.Exceptions;

namespace MyBlog.Contracts.Responses;

public class Result
{
    public Result(List<Error> errors, int statusCode)
    {
        Errors = errors;
        StatusCode = statusCode;
    }

    public List<Error> Errors { get; set; } = new List<Error>();

    public int StatusCode { get; set; } = StatusCodes.Status200OK;

    public bool IsSuccess
    {
        get
        {
            return !this.Errors.Any();
        }
    }

    public static Result Success(int? statusCode = null)
    {
        return new(new List<Error>(), statusCode ?? StatusCodes.Status200OK);
    }

    public static Result Failure(List<Error> errors, int statusCode)
    {
        return new(errors, statusCode);
    }

    public static Result Failure(Error error, int statusCode)
    {
        return new(new List<Error>() { error }, statusCode);
    }
}