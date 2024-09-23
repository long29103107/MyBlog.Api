using Microsoft.AspNetCore.Http;
using MyBlog.Contracts.Dtos.Interfaces;
using System.Text.Json.Serialization;

namespace Contracts.Dtos;

public class ListResponse<T> : IResponse where T : class
{
    [JsonIgnore]
    public int StatusCode { get; set; } = StatusCodes.Status200OK;
    public int Count { get; set; }
    public List<T> Results { get; set; }
}