using LonGBlog.Shared.Contract.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LonGBlog.Shared.Contract;

public class ListResponse<T> : IResponse where T : class
{
    [JsonIgnore]
    public int StatusCode { get; set; } = StatusCodes.Status200OK;
    public int Count { get; set; }
    public List<T> Results { get; set; }
}