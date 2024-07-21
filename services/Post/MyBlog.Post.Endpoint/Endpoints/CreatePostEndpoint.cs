using FastEndpoints;
using MyBlog.Post.Service.Dtos.Post;
using MyBlog.Post.Service.Interfaces;

namespace LonGMusik.Song.Endpoint.Endpoints;

public class CreatePostEndpoint : Endpoint<CreatePostRequest, PostResponse>
{
    private readonly IPostService _postService;

    public CreatePostEndpoint(IPostService postService)
    {
        _postService = postService;
    }

    public override void Configure()
    {
        Post("/api/posts");
        Options(x => x.WithTags("Post"));
        Tags("Post");
    }

    public override async Task HandleAsync(CreatePostRequest request, CancellationToken ct)
    {
        var response = await _postService.CreatePostAsync(request);

        await SendOkAsync(response, ct);
    }
}