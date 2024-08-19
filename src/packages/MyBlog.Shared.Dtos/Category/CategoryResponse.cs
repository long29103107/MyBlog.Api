using MyBlog.Contracts.Domains.ValueOf;

namespace MyBlog.Shared.Databases.Category;

public sealed record CategoryResponse
{
    public CategoryId Id { get; set; }
    public string Name { get; set; }
    public string SlugName { get; set; }
}