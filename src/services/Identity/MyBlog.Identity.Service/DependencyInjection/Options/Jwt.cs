namespace MyBlog.Identity.Service.DependencyInjection.Options;

public sealed class JWT
{
    public string ValidAudience { get; set; }
    public string ValidIssuer { get; set; }
    public string Secret { get; set; }
}

