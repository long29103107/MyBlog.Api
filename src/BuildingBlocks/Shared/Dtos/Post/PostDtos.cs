namespace Shared.Dtos.Post;

public static class PostDtos
{
    //Request
    public sealed record PostCreateRequest(string Title, string Content);
    public sealed record PostUpdatePartialRequest(string Title, string Content);
    public sealed record PostUpdateRequest(string Title, string Content);

    //Response
    public sealed record PostListResponse(int Id, string Title, string Content);
    public sealed record PostResponse(int Id, string Content);
}