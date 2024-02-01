using Gradeo.CoreApplication.Domain.Enums;
using Gradeo.CoreApplication.Domain.Interfaces;

namespace Gradeo.CoreApplication.Domain.Entities;

public class Role : BaseEntity, IEntityIdentity, ISoftDeletable
{
    public int Id { get; set; }
    public string RoleName { get; set; }

    public int? BusinessUnitId { get; set; }
    public BusinessUnitType BusinessUnitType { get; set; }
    public bool IsDefault { get; set; }
    public bool IsDeleted { get; set; }
    public BusinessUnit? BusinessUnit { get; set; }
    public List<UserRole>? UserRoles { get; set; }
    public List<RolePermission> Permissions { get; set; }
}