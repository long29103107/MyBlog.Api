using FastEndpoints;
using MyBlog.Post.Service.Dtos.Post;
using MyBlog.Post.Service.Implements;
using MyBlog.Post.Service.Interfaces;

namespace LonGMusik.Song.Endpoint.Endpoints;

public class UpdatePostEndpoint : Endpoint<UpdatePostRequest, PostResponse>
{
    private readonly IPostService _postService;

    public UpdatePostEndpoint(IPostService postService)
    {
        _postService = postService;
    }

    public override void Configure()
    {
        Put("/api/posts/{id:int}");
        Options(x => x.WithTags("Post"));
        Tags("Post");
    }

    public override async Task HandleAsync(UpdatePostRequest request, CancellationToken ct)
    {
        var response = await _postService.UpdatePostAsync(request);

        await SendOkAsync(response, ct);
    }
}