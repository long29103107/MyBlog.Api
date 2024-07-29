using Newtonsoft.Json;

namespace LonGBlog.Shared.Contract.Interfaces;

public interface IResponse
{
    [JsonIgnore]
    int StatusCode { get; set; }
}