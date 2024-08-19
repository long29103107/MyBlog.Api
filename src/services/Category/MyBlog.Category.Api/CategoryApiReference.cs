using System.Reflection;

namespace MyBlog.Category.Repository;
public static class CategoryApiReference
{
    public static readonly Assembly Assembly = typeof(CategoryApiReference).Assembly;
    public static readonly string AssemblyName = typeof(CategoryApiReference).Assembly.GetName().Name;
}