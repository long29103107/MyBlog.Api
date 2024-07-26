using System.Reflection;

namespace MyBlog.Post.Service;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    public static readonly string AssemblyName = typeof(AssemblyReference).Assembly.GetName().Name;
}