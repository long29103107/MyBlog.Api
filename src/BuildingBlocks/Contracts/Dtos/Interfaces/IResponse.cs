using Newtonsoft.Json;

namespace MyBlog.Contracts.Dtos.Interfaces;

public interface IResponse
{
    [JsonIgnore]
    int StatusCode { get; set; }
}