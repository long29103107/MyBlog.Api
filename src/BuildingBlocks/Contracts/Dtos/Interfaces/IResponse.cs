using System.Text.Json.Serialization;

namespace MyBlog.Contracts.Dtos.Interfaces;

public interface IResponse
{
    [JsonIgnore]
    int StatusCode { get; set; }
}