using MyBlog.Contracts.Dtos;
using System.Reflection;

namespace FilteringAndSortingExpression.Extensions;

public static partial class LinqExtensions
{
    public static IQueryable<T> Sort<T>(this IQueryable<T> query, ListRequest dto) where T : class
    {
        return query.OrderBy(dto.OrderBy, dto.OrderDesc);
    }

    public static List<string> GetPropertiesAsString<T>()
    {
        IList<PropertyInfo> props = new List<PropertyInfo>(typeof(T).GetProperties());
        return props.Select(prop => prop.Name.ToLower()).ToList();
    }

    public static string GetPropertiesDefaultSortAsString<T>(string defaultSort = "id")
    {
        IList<PropertyInfo> props = new List<PropertyInfo>(typeof(T).GetProperties());

        if(props.Any(x => x.Name.ToLower() == defaultSort))
        {
            return defaultSort;
        }

        return props.FirstOrDefault()?.Name.ToLower() ?? string.Empty;
    }

    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T> source)
        where T : class
    {
        if (source.IsNullOrEmpty())
        {
            return source;
        }

        return source.Where(x => x != null);
    }

    public static IQueryable<T> WhereNotNull<T>(this IQueryable<T> source)
        where T : class
    {
        if (source == null)
        {
            return source;
        }

        return source.Where(x => x != null);
    }
}