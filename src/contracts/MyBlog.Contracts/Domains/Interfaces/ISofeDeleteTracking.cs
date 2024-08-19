namespace MyBlog.Contracts.Domains.Interfaces;

public interface ISofeDeleteTracking
{
    DateTimeOffset? DeletedAt { get; set; }
    string DeletedBy { get; set; }
}