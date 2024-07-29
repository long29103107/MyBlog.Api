namespace MyBlog.Shared.Databases.Category;

public sealed record ListCategoryResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string SlugName { get; set; }
}