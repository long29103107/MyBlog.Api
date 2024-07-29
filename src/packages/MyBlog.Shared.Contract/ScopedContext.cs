using LonGBlog.Shared.Contract.Interfaces;
using System.Collections.Specialized;

namespace LonGBlog.Shared.Contract;

public class ScopedContext : IScopedContext
{
    public string TransactionId { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string AccessToken { get; set; }
    public NameValueCollection RequestQueryString { get; set; }
    public string RequestPath { get; set; }
}