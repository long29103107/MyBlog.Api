namespace MyBlog.Contracts.Domains.Interfaces;

public interface ISoftDeletableTracking
{
    DateTimeOffset? DeletedAt { get; set; }
    string DeletedBy { get; set; }
}