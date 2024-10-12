using System.Reflection;

namespace MyBlog.Identity.Repository;
public static class IdentityRepositoryReference
{
    public static readonly Assembly Assembly = typeof(IdentityRepositoryReference).Assembly;
    public static readonly string AssemblyName = typeof(IdentityRepositoryReference).Assembly.GetName().Name;
}