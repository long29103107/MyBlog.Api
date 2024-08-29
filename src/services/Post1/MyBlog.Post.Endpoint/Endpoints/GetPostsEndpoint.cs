using FastEndpoints;
using MyBlog.Post.Service.Dtos.Post;
using MyBlog.Post.Service.Interfaces;
using LonGBlog.Shared.Contract;
namespace LonGMusik.Song.Endpoint.Endpoints;

public class GetPostsEndpoint : Endpoint<BindingPagingPostRequest, PagingListResponse<PostResponse>>
{
    private readonly IPostService _postService;

    public GetPostsEndpoint(IPostService postService)
    {
        _postService = postService;
    }

    public override void Configure()
    {
        Get("/api/posts");
        Options(x => x.WithTags("Post"));
        Tags("Post");
    }

    public override async Task HandleAsync(BindingPagingPostRequest request, CancellationToken ct)
    {
        var response = await _postService.GetPostsAsync(request);

        await SendOkAsync(response, ct);
    }
}