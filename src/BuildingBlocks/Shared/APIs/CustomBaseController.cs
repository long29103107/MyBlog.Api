using Contracts.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Contracts.Dtos.Interfaces;

namespace Shared.APIs;

[Route("api/[controller]")]
[ApiController]

public abstract class CustomControllerBase : ControllerBase
{
    protected IScopedCache _scopedCache;

    protected IActionResult GetResponse(bool res)
    {
        return _GetResponse(StatusCodes.Status200OK, res);
    }

    protected IActionResult GetResponse(int? statusCode = null)
    {
        return _GetResponse(statusCode ?? StatusCodes.Status200OK, null);
    }

    protected IActionResult GetResponse(int statusCode, object res)
    {
        return _GetResponse(statusCode, res);
    }

    protected IActionResult GetResponse<T>(T res) where T : IResponse
    {
        return _GetResponse(res.StatusCode, res);
    }

    protected IActionResult GetResponse<T>(int statusCode, IEnumerable<T> res) where T : IResponse
    {
        return _GetResponse(statusCode, res);
    }

    protected IActionResult GetResponse<T>(IEnumerable<T> res) where T : IResponse
    {
        return _GetResponse(StatusCodes.Status200OK, res);
    }

    private IActionResult _GetResponse(int statusCode, object res)
        => statusCode switch
        {
            StatusCodes.Status200OK => Ok(res),
            StatusCodes.Status201Created => Created(string.Empty, res),
            StatusCodes.Status204NoContent => NoContent(),
            StatusCodes.Status404NotFound => NotFound(res),
            StatusCodes.Status400BadRequest => BadRequest(res),
            StatusCodes.Status401Unauthorized => Unauthorized(res),
            StatusCodes.Status409Conflict => Conflict(res),
            _ => Forbid()
        };
}