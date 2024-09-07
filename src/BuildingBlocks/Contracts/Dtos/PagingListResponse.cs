using Contracts.Abstractions.Shared;

namespace Contracts.Dtos;

public class PagingListResponse<T> : ResponseListResult<T> where T : class
{
    public PagingListResponse()
    {
    }

    public PagingListResponse(List<Error> errors, List<T> results, int statusCode) : base(errors, results, statusCode)
    {
        if (!errors.Any())
        {
            Results = results ?? new List<T>();
        }
    }

    public PagingListResponse(PagingListResponse<T> response)
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

    public static PagingListResponse<T> Success(PagingListResponse<T> response)
    {
        return new(response);
    }

    public static PagingListResponse<T> Failure(List<Error> errors, int statusCode)
    {
        return new(errors, new List<T>(), statusCode);
    }

    public static PagingListResponse<T> Failure(Error error, int statusCode)
    {
        return new(new List<Error>() { error }, new List<T>(), statusCode);
    }
}
