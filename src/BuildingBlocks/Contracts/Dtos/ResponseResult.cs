using Contracts.Abstractions.Shared;
using Microsoft.AspNetCore.Http;

namespace Contracts.Dtos;

public class ResponseResult
{
    public ResponseResult(List<Error> errors,  int statusCode)
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

    public static ResponseResult Success(int? statusCode = null)
    {
        return new (new List<Error>(), statusCode ?? StatusCodes.Status200OK);
    }

    public static ResponseResult Failure(List<Error> errors, int statusCode)
    {
        return new(errors, statusCode);
    }

    public static ResponseResult Failure(ResponseResult responseResult)
    {
        return new(responseResult.Errors, responseResult.StatusCode);
    }

    public static ResponseResult Failure(Error error, int statusCode)
    {
        return new(new List<Error>() { error }, statusCode);
    }
}