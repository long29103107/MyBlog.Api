using System.Collections.Specialized;

namespace MyBlog.Contracts.Dtos.Interfaces;

public interface IScopedContext
{
    //public string TransactionId { get; set; }
    //public string UserId { get; set; }
    //public string UserName { get; set; }
    //public string AccessToken { get; set; }
    public NameValueCollection RequestQueryString { get; set; }
    public string RequestPath { get; set; }
}

