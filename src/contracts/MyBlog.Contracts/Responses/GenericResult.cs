using Microsoft.AspNetCore.Http;

namespace MyBlog.Contracts.Responses;

public class Result<T> : Result where T : class
{
    public Result() : base([], StatusCodes.Status200OK)
    {
    }

    public Result(List<Error> errors, T result, int statusCode) : base(errors, StatusCodes.Status200OK)
    {
        if (!errors.Any())
        {
            Data = result ?? (T)Activator.CreateInstance(typeof(T));
        }
    }

    public T Data { get; set; }

    public static Result<T> Success(T result)
    {
        return new(new List<Error>(), result, StatusCodes.Status200OK);
    }

    public static Result<T> Success(T result, int? statusCode = null)
    {
        return new(new List<Error>(), result, statusCode ?? StatusCodes.Status200OK);
    }

    public static Result<T> Success(int? statusCode = null)
    {
        return new(new List<Error>(), default(T), statusCode ?? StatusCodes.Status200OK);
    }

    public static Result<T> Failure(List<Error> errors, int statusCode)
    {
        return new(errors, default(T), statusCode);
    }

    public static Result<T> Failure(Error error, int statusCode)
    {
        return new(new List<Error>() { error }, default(T), statusCode);
    }
}