using System.Reflection;

namespace MyBlog.Shared.Databases;
public static class SharedDatabaseReference
{
    public static readonly Assembly Assembly = typeof(SharedDatabaseReference).Assembly;
    public static readonly string AssemblyName = typeof(SharedDatabaseReference).Assembly.GetName().Name;
}