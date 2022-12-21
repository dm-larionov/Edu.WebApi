using Edu.WebApi.Shared.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace Edu.WebApi.Infrastructure.Auth.Permissions;

public class MustHavePermissionAttribute : AuthorizeAttribute
{
    public MustHavePermissionAttribute(string action, string resource) =>
        Policy = EduPermission.NameFor(action, resource);
}