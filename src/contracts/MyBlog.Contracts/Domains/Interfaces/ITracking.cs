namespace MyBlog.Contracts.Domains.Interfaces;

public interface ITracking
{
    DateTimeOffset CreatedAt { get; set; }
    string CreatedBy { get; set; }
    DateTimeOffset? UpdatedAt { get; set; }
    string UpdatedBy { get; set; }
    DateTimeOffset? DeletedAt { get; set; }
    string DeletedBy { get; set; }
}

