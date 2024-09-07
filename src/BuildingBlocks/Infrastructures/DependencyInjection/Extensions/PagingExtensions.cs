using Contracts.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.DependencyInjection.Extensions;

public static class PagingExtensions
{
    public static PagingListResponse<T> GetMakeList<T>(this List<T> queryset, PagingListRequest request)
        where T : class
    {
        var result = new PagingListResponse<T>();
        if (!queryset.Any()) return result;

        result.Page = request.Page;
        result.PageSize = request.PageSize;
        result.RowCount = queryset.Count();

        var pageCount = (double)result.RowCount / result.PageSize;
        result.Count = (int)Math.Ceiling(pageCount);

        var skip = (result.Page - 1) * result.PageSize;
        var take = result.PageSize;

        result.Results = queryset
            .Skip(skip)
            .Take(take)
            .ToList();

        return PagingListResponse<T>.Success(result);
    }

    public async static Task<PagingListResponse<T>> GetMakeListAsync<T>(this IQueryable<T> queryset, PagingListRequest request)
    where T : class
    {
        var result = new PagingListResponse<T>();
        if (!await queryset.AnyAsync()) return result;

        result.Page = request.Page;
        result.PageSize = request.PageSize;
        result.RowCount = await queryset.CountAsync();

        var pageCount = (double)result.RowCount / result.PageSize;
        result.Count = (int)Math.Ceiling(pageCount);

        var skip = (result.Page - 1) * result.PageSize;
        var take = result.PageSize;

        result.Results = await queryset
            .Skip(skip)
            .Take(take)
            .ToListAsync();

        return PagingListResponse<T>.Success(result);
    }
}