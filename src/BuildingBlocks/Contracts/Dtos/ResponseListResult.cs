using Contracts.Abstractions.Shared;
using Microsoft.AspNetCore.Http;

namespace Contracts.Dtos;

public class ResponseListResult<T> : ResponseResult
{ 
    public ResponseListResult() : base(new List<Error>(), StatusCodes.Status200OK)
    {
    }

    public ResponseListResult(List<Error> errors, List<T> results, int statusCode) : base(errors, StatusCodes.Status200OK)
    {
        if (!errors.Any())
        {
            Results = results ?? new List<T>();
        }
    }

    public static ResponseListResult<T> Success(List<T>? results)
    {
        return new(new List<Error>(), results ?? new List<T>(), StatusCodes.Status200OK);
    }

    public static ResponseListResult<T> Success(List<T> results, int? statusCode = null)
    {
        return new(new List<Error>(), results ?? new List<T>(), statusCode ?? StatusCodes.Status200OK);
    }

    public static ResponseListResult<T> Success(int? statusCode = null)
    {
        return new(new List<Error>(), new List<T>(), statusCode ?? StatusCodes.Status200OK);
    }

    public static ResponseListResult<T> Failure(List<Error> errors, int statusCode)
    {
        return new(errors, new List<T>(), statusCode);
    }

    public static ResponseListResult<T> Failure(Error error, int statusCode)
    {
        return new(new List<Error>() { error }, new List<T>(), statusCode);
    }

    public List<T> Results { get; set; }
}