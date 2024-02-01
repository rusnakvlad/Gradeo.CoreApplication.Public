using Gradeo.CoreApplication.Domain.Enums;

namespace Gradeo.CoreApplication.Application.Common.Helpers;

public static class PermissionHelper
{
    public static void ValidatePermissions (IEnumerable<Permission> userPermission, Permission requiredPermission)
    {
        if(!userPermission.Contains(requiredPermission))
        {
            throw new UnauthorizedAccessException("User does not have required permissions for this request");
        }
    }
}