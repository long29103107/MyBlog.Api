namespace LonGBlog.Shared.Contract.Helpers;

public static class PermissionHelper
{
    public static string ConvertOperation(string scope, string action)
        => $"{scope}.{action}";
}