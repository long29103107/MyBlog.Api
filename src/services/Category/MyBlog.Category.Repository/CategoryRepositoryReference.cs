using System.Reflection;

namespace MyBlog.Category.Domain;
public static class CategoryRepositoryReference
{
    public static readonly Assembly Assembly = typeof(CategoryRepositoryReference).Assembly;
    public static readonly string AssemblyName = typeof(CategoryRepositoryReference).Assembly.GetName().Name;
}