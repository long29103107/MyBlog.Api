using Contracts.Abstractions.Shared;
using Microsoft.AspNetCore.Http;

namespace Contracts.Dtos;

public class ResponseResult<T> : ResponseResult 
{
    public ResponseResult() : base(new List<Error>(), StatusCodes.Status200OK)
    {
    }

    public ResponseResult(List<Error> errors, T result, int statusCode) : base(errors, statusCode)
    {
        if (!errors.Any())
        {
            Result = result ?? (T)Activator.CreateInstance(typeof(T));
        }
    }

    public static ResponseResult<T> Success(T result)
    {
        return new(new List<Error>(), result, StatusCodes.Status200OK);
    }

    public static ResponseResult<T> Success(T result, int? statusCode = null)
    {
        return new(new List<Error>(), result, statusCode ?? StatusCodes.Status200OK);
    }

    public static ResponseResult<T> Success(int? statusCode = null)
    {
        return new(new List<Error>(), default(T), statusCode ?? StatusCodes.Status200OK);
    }

    public static ResponseResult<T> Failure(List<Error> errors, int statusCode)
    {
        return new(errors, default(T), statusCode);
    }

    public static ResponseResult<T> Failure(Error error, int statusCode)
    {
        return new(new List<Error>() { error }, default(T), statusCode);
    }

    public T Result { get; set; }
}