using FilteringAndSortingExpression.Extensions;
using Contracts.Dtos;

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

    public sealed class PostListRequest : ListRequest
    {
        public override List<string> PropertiesWhiteList
           => LinqExtensions.GetPropertiesAsString<PostResponse>();

        /// <summary>
        ///     Sort set: All fiels in response
        /// </summary>
        public override string Sort { get; set; }
            = LinqExtensions.GetPropertiesDefaultSortAsString<PostResponse>($"-{nameof(PostResponse.Id).ToLower()}");
    }

    public sealed class PagingPostRequest : PagingListRequest
    {
        public override List<string> PropertiesWhiteList
           => LinqExtensions.GetPropertiesAsString<PostResponse>();

        /// <summary>
        ///     Sort set: All fiels in response
        /// </summary>
        public override string Sort { get; set; }
            = LinqExtensions.GetPropertiesDefaultSortAsString<PostResponse>($"-{nameof(PostResponse.Id).ToLower()}");
    }
}