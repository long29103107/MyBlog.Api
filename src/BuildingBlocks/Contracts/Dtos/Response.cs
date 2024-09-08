using Microsoft.AspNetCore.Http;
using MyBlog.Contracts.Dtos.Interfaces;
using Newtonsoft.Json;

namespace Contracts.Dtos;

public abstract class Response : IResponse
{
    [JsonIgnore]
    public int StatusCode { get; set; } = StatusCodes.Status200OK;
}