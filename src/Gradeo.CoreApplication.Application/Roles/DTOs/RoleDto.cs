using Gradeo.CoreApplication.Application.Permissions.DTOs;

namespace Gradeo.CoreApplication.Application.Roles.DTOs;

public class RoleDto
{
    public int Id { get; set; }
    public string RoleName { get; set; }
    public bool IsDefault { get; set; }

    public int? BusinessUnitId { get; set; }
    public List<PermissionDto> Permissions { get; set; }
}