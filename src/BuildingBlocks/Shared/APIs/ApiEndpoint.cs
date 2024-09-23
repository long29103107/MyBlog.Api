using Contracts.Abstractions.Shared;
using Contracts.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shared.APIs;

public abstract class ApiEndpoint
{
    protected static IResult HandlerFailure(ResponseResult result) =>
        result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(),
            //ValidationResult validationResult =>
            //    Results.BadRequest(
            //        CreateProblemDetails(
            //            "Validation Error", StatusCodes.Status400BadRequest,
            //result.Error,
            //            validationResult.Errors)),
            _ =>
                Results.BadRequest(
                    CreateProblemDetails(
                        "Bab Request", StatusCodes.Status400BadRequest,
                        null))
        };

    private static ProblemDetails CreateProblemDetails(string title, int status, Error error, Error[]? errors = null)
        => new()
        {
            Title = title,
            Type = error.Code,
            Detail = error.Message,
            Status = status,
            Extensions = { { nameof(errors), errors } }
        };
}