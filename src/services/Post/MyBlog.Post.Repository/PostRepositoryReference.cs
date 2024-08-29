using System.Reflection;

namespace MyBlog.Post.Domain;
public static class PostRepositoryReference
{
    public static readonly Assembly Assembly = typeof(PostRepositoryReference).Assembly;
    public static readonly string AssemblyName = typeof(PostRepositoryReference).Assembly.GetName().Name;
}