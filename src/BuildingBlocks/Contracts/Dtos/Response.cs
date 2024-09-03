using Microsoft.AspNetCore.Http;
using MyBlog.Contracts.Dtos.Interfaces;
using System.Text.Json.Serialization;

namespace MyBlog.Contracts.Dtos;

public class Response : IResponse
{
    [JsonIgnore]
    public virtual int StatusCode { get; set; } = StatusCodes.Status200OK;
}