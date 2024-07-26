using FastEndpoints;
using MyBlog.Post.Service.Dtos.Post;
using MyBlog.Post.Service.Interfaces;

namespace LonGMusik.Song.Endpoint.Endpoints;

public class DeletePostEndpoint : Endpoint<DeletePostRequest>
{
    private readonly IPostService _postService;

    public DeletePostEndpoint(IPostService postService)
    {
        _postService = postService;
    }

    public override void Configure()
    {
        Delete("/api/posts/{id:int}");
        Options(x => x.WithTags("Post"));
        Tags("Post");
    }

    public override async Task HandleAsync(DeletePostRequest request, CancellationToken ct)
    {
        await _postService.DeletePostAsync(request);

        await SendOkAsync(ct);
    }
}