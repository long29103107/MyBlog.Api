
using Contracts.Abstractions.Shared;
using Contracts.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Shared.APIs;

[Route("api/[controller]")]
[ApiController]
public class CustomResultBaseController : ControllerBase
{
    public CustomResultBaseController()
    {
    }

    protected IActionResult GetResponse(ResponseResult result)
    {
        return Ok(result);
    }

    protected IActionResult GetResponse<T>(ResponseResult<T> result)
    {
        return Ok(result);
    }

    protected IActionResult GetResponse(ResponseResult result, Request request)
    {
        if (ModelState.IsValid)
            return Ok(result);
        else
            return _GetFailedModel(ModelState);
    }

    protected IActionResult GetResponse<T>(ResponseResult<T> result, Request request)
    {
        if (ModelState.IsValid)
            return Ok(result);
        else
            return _GetFailedModel(ModelState);
    }

    protected IActionResult GetResponse(ResponseResult result, ListRequest request)
    {
        if (ModelState.IsValid)
            return Ok(result);
        else
            return _GetFailedModel(ModelState);
    }


    protected IActionResult GetResponse<T>(ResponseResult<T> result, ListRequest request)
    {
        if (ModelState.IsValid)
            return Ok(result);
        else
            return _GetFailedModel(ModelState);
    }

    protected IActionResult GetResponse(ResponseResult result, PagingListRequest request)
    {
        if (ModelState.IsValid)
            return Ok(result);
        else
            return _GetFailedModel(ModelState);
    }


    protected IActionResult GetResponse<T>(ResponseResult<T> result, PagingListRequest request)
    {
        if (ModelState.IsValid)
            return Ok(result);
        else
            return _GetFailedModel(ModelState);
    }


    protected IActionResult _GetFailedModel(ModelStateDictionary modelState)
    {
        var errors = ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => new Error(e.Exception?.GetType().Name ?? "ValidationError", e.ErrorMessage))
            .ToList();

        return Ok(ResponseResult.Failure(errors, StatusCodes.Status400BadRequest));
    }
}