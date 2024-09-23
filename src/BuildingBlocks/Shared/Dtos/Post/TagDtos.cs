using FilteringAndSortingExpression.Extensions;
using Contracts.Dtos;
using static Shared.Dtos.Post.PostDtos;
using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos.Post;

public static class TagDtos
{
    //Request
    public sealed class TagCreateRequest : Request
    {
        public string Name { get; set; }
    }
    public sealed class TagUpdatePartialRequest : Request
    {
        public string Name { get; set; }
    }
    public sealed class TagUpdateRequest : Request
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    //Response
    public sealed class TagListResponse : Response
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public sealed class TagResponse : Response
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PostResponse>? Posts { get; set; }
    }

    public sealed class TagListRequest : ListRequest
    {
        public override List<string> PropertiesWhiteList
           => LinqExtensions.GetPropertiesAsString<TagResponse>();

        /// <summary>
        ///     Sort set: All fiels in response
        /// </summary>
        public override string Sort { get; set; }
            = LinqExtensions.GetPropertiesDefaultSortAsString<TagResponse>($"-{nameof(TagResponse.Id).ToLower()}");
    }
}