using FilteringAndSortingExpression.Extensions;
using Contracts.Dtos;
using static Shared.Dtos.Post.PostDtos;
using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos.Post;

public static class CategoryDtos
{
    //Request
    public sealed class CategoryCreateRequest : Request
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public int? ParentCategoryId { get; set; }
    }
    public sealed class CategoryUpdatePartialRequest : Request
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentCategoryId { get; set; }
    }
    public sealed class CategoryUpdateRequest : Request
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentCategoryId { get; set; }
    }

    //Response
    public sealed class CategoryListResponse : Response
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentCategoryId { get; set; }
    }

    public sealed class CategoryResponse : Response
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentCategoryId { get; set; }
        public List<PostResponse>? Posts { get; set; }
    }

    public sealed class CategoryListRequest : ListRequest
    {
        public override List<string> PropertiesWhiteList
           => LinqExtensions.GetPropertiesAsString<CategoryResponse>();

        /// <summary>
        ///     Sort set: All fiels in response
        /// </summary>
        public override string Sort { get; set; }
            = LinqExtensions.GetPropertiesDefaultSortAsString<CategoryResponse>($"-{nameof(CategoryResponse.Id).ToLower()}");
    }
}