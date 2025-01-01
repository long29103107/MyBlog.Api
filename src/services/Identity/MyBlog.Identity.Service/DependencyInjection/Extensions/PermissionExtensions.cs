using MyBlog.Identity.Domain.Entities;

namespace MyBlog.Identity.Service.DependencyInjection.Extensions;

public static class PermissionExtensions
{
    public static string GetPermissionCode(this Permission permission)
    {
        if (permission?.Operation?.Code is null || permission?.Scope?.Code is null)
        {
            return string.Empty;
        }

        return $"{permission.Scope.Code}.{permission.Operation.Code}";
    }

    public static string GetPermissionName(this Permission permission)
    {
        if (permission?.Operation?.Name is null || permission?.Scope?.Name is null)
        {
            return string.Empty;
        }

        return $"{permission.Scope.Name} {permission.Operation.Name}";
    }
}

