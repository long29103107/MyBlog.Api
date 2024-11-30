using Contracts.Dtos;
using FilteringAndSortingExpression.Extensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Dtos.Identity.Role;

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

    public sealed class RoleCreateRequest : Request
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [NotMapped]
        public bool IsLocked { get; } = false;
    }

    public sealed class RoleUpdatePartialRequest : Request
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
    }

    public sealed class RoleUpdateRequest : Request
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
    }

    public sealed class RoleDeleteRequest : Request
    {
        public int Id { get; set; }
    }
    #endregion

    #region Response
    public sealed class PermissionListResponse : Response
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; } = string.Empty!;
        public string UpdatedBy { get; set; } = string.Empty!;
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsLocked { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public sealed class PermissionResponse : Response
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; } = string.Empty!;
        public string UpdatedBy { get; set; } = string.Empty!;
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    #endregion
}