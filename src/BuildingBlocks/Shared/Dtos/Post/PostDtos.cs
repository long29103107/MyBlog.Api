using FilteringAndSortingExpression.Extensions;
using Contracts.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos.Post;

public static class PostDtos
{
    //Request
    public sealed class PostCreateRequest : Request
    {
        [Required]
        public string Title { get; set; }
        public string Content { get; set; }
    }

    public sealed class PostUpdatePartialRequest : Request
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }

    public sealed class PostUpdateRequest : Request
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }

    public sealed class PostDeleteRequest : Request
    {
        public int Id { get; set; }
    }

    //Response
    public sealed class PostListResponse : Response
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
    public sealed class PostResponse : Response
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }

    public sealed class PostDeleteResponse : Response
    {
        public int Id { get; set; }
    }

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