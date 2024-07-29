using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel;

namespace LonGBlog.Shared.Contract;

public abstract class BindingPagingBaseRequest
{
    [FromQuery]
    public int Page { get; set; } = 1;
    [FromQuery]
    public int PageSize { get; set; } = 50;
    [FromQuery]
    [JsonProperty("fe")]
    public string? FilterExp { get; set; }
    [FromQuery]
    public string? Sort { get; set; }
}