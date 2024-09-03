using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyBlog.Contracts.Dtos.Interfaces;
using System.Text.Json.Serialization;

namespace MyBlog.Contracts.Dtos;


public abstract class Request : IRequest
{
    [JsonIgnore]
    [BindNever]
    //[SwaggerIgnore]
    public IScopedContext? ScopedContext { get; set; }
}