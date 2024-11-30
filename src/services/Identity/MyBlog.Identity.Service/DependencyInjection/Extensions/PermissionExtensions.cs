using MyBlog.Identity.Domain.Entities;

namespace MyBlog.Identity.Service.DependencyInjection.Extensions;

public static class PermissionExtensions
{
    public static string GetPermission(this Permission permission)
    {
        if (permission?.Operation?.Code is null || permission?.Scope?.Code is null)
        {
            return string.Empty;
        }

        return $"{permission.Scope.Code}.{permission.Operation.Code}";
    }
}

