using Microsoft.AspNetCore.Http;

namespace Contracts.Abstractions.Shared;

public class Response
{
    public Response(List<Error> errors,  int statusCode)
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

    public static Response Success(int? statusCode = null)
    {
        return new (new List<Error>(), statusCode ?? StatusCodes.Status200OK);
    }

    public static Response Failure(List<Error> errors, int statusCode)
    {
        return new(errors, statusCode);
    }

    public static Response Failure(Error error, int statusCode)
    {
        return new(new List<Error>() { error }, statusCode);
    }
}