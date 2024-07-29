using LonGBlog.Shared.Contract.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LonGBlog.Shared.Contract;

public class Response : IResponse
{
    [JsonIgnore]
    public virtual int StatusCode { get; set; } = StatusCodes.Status200OK;
}