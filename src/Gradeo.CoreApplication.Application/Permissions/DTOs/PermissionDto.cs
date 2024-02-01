using Gradeo.CoreApplication.Domain.Enums;

namespace Gradeo.CoreApplication.Application.Permissions.DTOs;

public class PermissionDto
{
    public Permission PermissionId { get; set; }
    public string Name => PermissionId.ToString();
}