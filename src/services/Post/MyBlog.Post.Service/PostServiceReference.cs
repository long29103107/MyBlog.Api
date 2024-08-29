using System.Reflection;

namespace MyBlog.Post.Service;
public static class PostServiceReference
{
    public static readonly Assembly Assembly = typeof(PostServiceReference).Assembly;
    public static readonly string AssemblyName = typeof(PostServiceReference).Assembly.GetName().Name;
}