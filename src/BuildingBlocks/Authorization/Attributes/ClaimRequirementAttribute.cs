using Microsoft.AspNetCore.Authorization;

namespace Authorization.Attributes;

public class ClaimRequirementAttribute : IAuthorizationRequirement
{
    public ClaimRequirementAttribute(string condition) => Condition = condition;
    public string Condition { get; set; }
}
