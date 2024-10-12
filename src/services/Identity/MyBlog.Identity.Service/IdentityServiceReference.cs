using System.Reflection;

namespace MyBlog.Identity.Service;
public static class IdentityServiceReference
{
    public static readonly Assembly Assembly = typeof(IdentityServiceReference).Assembly;
    public static readonly string AssemblyName = typeof(IdentityServiceReference).Assembly.GetName().Name;
}