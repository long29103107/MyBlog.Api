namespace LonGBlog.Shared.Contract.Caches;

public class ScopedCache : IScopedCache
{
    public ScopedContext ScopedContext { get; set; } = new ScopedContext();
}

public interface IScopedCache 
{
    ScopedContext ScopedContext { get; set; } 
    //bool StartTransaction(string tags);
    //bool IsInTransaction { get; }
    //string TransactionTags { get; }
    //Dictionary<string, object> Values { get; }
}