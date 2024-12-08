using Contracts.Dtos;
using FilteringAndSortingExpression.Extensions;

namespace Shared.Dtos.Identity.Permission;

public static class PermissionDtos
{
    #region Request
    public sealed class PermissionListRequest : ListRequest
    {
        public override List<string> PropertiesWhiteList
           => LinqExtensions.GetPropertiesAsString<PermissionResponse>();

        /// <summary>
        ///     Sort set: All fiels in response
        /// </summary>
        public override string Sort { get; set; }
            = LinqExtensions.GetPropertiesDefaultSortAsString<PermissionResponse>(
                $"-{nameof(PermissionResponse.Id).ToLower()}"
            );
    }
    #endregion

    #region Response
    public sealed class PermissionListResponse : Response
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; } = string.Empty!;
        public string UpdatedBy { get; set; } = string.Empty!;
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public sealed class PermissionResponse : Response
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; } = string.Empty!;
        public string UpdatedBy { get; set; } = string.Empty!;
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    #endregion
}