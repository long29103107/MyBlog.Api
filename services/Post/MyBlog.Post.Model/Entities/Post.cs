namespace MyBlog.Post.Model.Entities;
public class Post : EntitiBase<int>
{
    public int Id { get; set; }
    public string Track { get; set; }
}
