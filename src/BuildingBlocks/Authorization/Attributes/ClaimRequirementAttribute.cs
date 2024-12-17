using Microsoft.AspNetCore.Authorization;

namespace Authorization.Attributes;

public class ClaimRequirementAttribute : IAuthorizationRequirement
{
    public string Scope { get; set; }
    public string Operation { get; set; }

    public ClaimRequirementAttribute(string scope, string operation)
    {
        Scope = scope;
        Operation = operation;
    }
    
}
