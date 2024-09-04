using FilteringAndSortingExpression.Extensions;
using MyBlog.Contracts.Dtos;

namespace Shared.Dtos.Category;

public static class CategoryDtos
{
    //Request
    public sealed record CategoryCreateRequest(string Name, string Description, int? ParentCategoryId);
    public sealed record CategoryUpdatePartialRequest(string Title, string Content);
    public sealed record CategoryUpdateRequest(string Title, string Content);

    //Response
    public sealed record CategoryListResponse(int Id, string Title, string Content);
    public sealed record CategoryResponse(int Id, string Name, string Description, int? ParentCategoryId);

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