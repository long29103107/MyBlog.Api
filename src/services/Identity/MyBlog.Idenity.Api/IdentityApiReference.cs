using System.Reflection;

namespace MyBlog.Identity.Api;
public static class IdentityApiReference
{
    public static readonly Assembly Assembly = typeof(IdentityApiReference).Assembly;
    public static readonly string AssemblyName = typeof(IdentityApiReference).Assembly.GetName().Name;
}