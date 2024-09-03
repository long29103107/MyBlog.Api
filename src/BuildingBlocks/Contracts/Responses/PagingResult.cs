namespace MyBlog.Contracts.Responses;

public class PagingResult<T> : Result<List<T>> where T : class
{
    public PagingResult()
    {
    }

    public PagingResult(List<Error> errors, List<T> result, int statusCode) : base(errors, result, statusCode)
    {
        if (!errors.Any())
        {
            Data = result ?? (List<T>)Activator.CreateInstance(typeof(List<T>));
        }
    }

    public PagingResult(PagingResult<T> response)
    {
        PageNumber = response.PageNumber;
        PageSize = response.PageSize;
        RowCount = response.RowCount;
        PageCount = response.PageCount;
        Data = response.Data;
    }

    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int RowCount { get; set; }
    public int PageCount { get; set; }

    public static PagingResult<T> Success(PagingResult<T> response)
    {
        return new(response);
    }

    public static PagingResult<T> Failure(List<Error> errors, int statusCode)
    {
        return new(errors, default(List<T>), statusCode);
    }

    public static PagingResult<T> Failure(Error error, int statusCode)
    {
        return new(new List<Error>() { error }, default(List<T>), statusCode);
    }
}
