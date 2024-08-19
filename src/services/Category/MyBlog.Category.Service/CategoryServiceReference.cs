using System.Reflection;

namespace MyBlog.Category.Service;
public static class CategoryServiceReference
{
    public static readonly Assembly Assembly = typeof(CategoryServiceReference).Assembly;
    public static readonly string AssemblyName = typeof(CategoryServiceReference).Assembly.GetName().Name;
}