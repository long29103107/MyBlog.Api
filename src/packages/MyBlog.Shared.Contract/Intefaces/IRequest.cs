using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace LonGBlog.Shared.Contract.Interfaces;

public interface IRequest
{
    ScopedContext? ScopedContext { get; set; }
}