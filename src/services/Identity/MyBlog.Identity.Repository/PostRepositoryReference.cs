using System.Reflection;

namespace MyBlog.Identity.Api;
public static class PostRepositoryReference
{
    public static readonly Assembly Assembly = typeof(PostRepositoryReference).Assembly;
    public static readonly string AssemblyName = typeof(PostRepositoryReference).Assembly.GetName().Name;
}