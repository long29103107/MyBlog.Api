﻿using System.Reflection;

namespace MyBlog.Post.Model;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    public static readonly string AssemblyName = typeof(AssemblyReference).Assembly.GetName().Name;
}