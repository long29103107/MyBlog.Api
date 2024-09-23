using Contracts.Abstractions.Shared;

namespace Contracts.Dtos;

public class PagingListResponseResult<T> : ResponseListResult<T> where T : class
{
    public PagingListResponseResult()
    {
    }

    public PagingListResponseResult(List<Error> errors, List<T> results, int statusCode) : base(errors, results, statusCode)
    {
        if (!errors.Any())
        {
            Results = results ?? new List<T>();
        }
    }

    public PagingListResponseResult(PagingListResponseResult<T> response)
    {
        Page = response.Page;
        PageSize = response.PageSize;
        RowCount = response.RowCount;
        Count = response.Count;
        Results = response.Results;
    }

    public int Page { get; set; }
    public int PageSize { get; set; }
    public int RowCount { get; set; }
    public int Count { get; set; }
    public string NextUrl { get; set; }
    public string PrevUrl { get; set; }

    public static PagingListResponseResult<T> Success(PagingListResponseResult<T> response)
    {
        return new(response);
    }

    public static PagingListResponseResult<T> Failure(List<Error> errors, int statusCode)
    {
        return new(errors, new List<T>(), statusCode);
    }

    public static PagingListResponseResult<T> Failure(Error error, int statusCode)
    {
        return new(new List<Error>() { error }, new List<T>(), statusCode);
    }
}
