using Microsoft.AspNetCore.Authorization;

namespace TD.WebApi.Infrastructure.Auth.Permissions;

internal class PermissionRequirement : IAuthorizationRequirement
{
    public string Permission { get; private set; }

    public PermissionRequirement(string permission)
    {
        Permission = permission;
    }
}