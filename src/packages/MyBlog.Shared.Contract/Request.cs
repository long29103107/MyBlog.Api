using FilteringAndSortingExpression.Swagger.Helpers;
using LonGBlog.Shared.Contract.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace LonGBlog.Shared.Contract;

public abstract class Request : IRequest
{
    [JsonIgnore]
    [BindNever]
    [SwaggerIgnore]
    public ScopedContext? ScopedContext { get; set; }
} 