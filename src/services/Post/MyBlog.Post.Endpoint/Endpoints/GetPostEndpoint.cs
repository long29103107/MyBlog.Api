using FastEndpoints;
using MyBlog.Post.Service.Dtos.Post;
using MyBlog.Post.Service.Interfaces;

namespace LonGMusik.Song.Endpoint.Endpoints;

public class GetPostEndpoint : Endpoint<PostRequest, PostResponse>
{
    private readonly IPostService _postService;

    public GetPostEndpoint(IPostService postService)
    {
        _postService = postService;
    }

    public override void Configure()
    {
        Get("/api/posts/{id:int}");
        Options(x => x.WithTags("Post"));
        Tags("Post");
    }

    public override async Task HandleAsync(PostRequest request, CancellationToken ct)
    {
        PostResponse response = await _postService.GetPostAsync(request);

        await SendOkAsync(response, ct);
    }
}