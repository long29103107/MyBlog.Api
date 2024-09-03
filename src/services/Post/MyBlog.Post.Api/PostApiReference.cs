using System.Reflection;

namespace MyBlog.Post.Api;
public static class PostApiReference
{
    public static readonly Assembly Assembly = typeof(PostApiReference).Assembly;
    public static readonly string AssemblyName = typeof(PostApiReference).Assembly.GetName().Name;
}