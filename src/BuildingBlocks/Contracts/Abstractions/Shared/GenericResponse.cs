using Microsoft.AspNetCore.Http;

namespace Contracts.Abstractions.Shared;

public class Response<T> : Response 
{
    public Response() : base(new List<Error>(), StatusCodes.Status200OK)
    {
    }

    public Response(List<Error> errors, T result, int statusCode) : base(errors, StatusCodes.Status200OK)
    {
        if (!errors.Any())
        {
            Result = result ?? (T)Activator.CreateInstance(typeof(T));
        }
    }

    public static Response<T> Success(T result)
    {
        return new(new List<Error>(), result, StatusCodes.Status200OK);
    }

    public static Response<T> Success(T result, int? statusCode = null)
    {
        return new(new List<Error>(), result, statusCode ?? StatusCodes.Status200OK);
    }

    public static Response<T> Success(int? statusCode = null)
    {
        return new(new List<Error>(), default(T), statusCode ?? StatusCodes.Status200OK);
    }

    public static Response<T> Failure(List<Error> errors, int statusCode)
    {
        return new(errors, default(T), statusCode);
    }

    public static Response<T> Failure(Error error, int statusCode)
    {
        return new(new List<Error>() { error }, default(T), statusCode);
    }

    public T Result { get; set; }
}