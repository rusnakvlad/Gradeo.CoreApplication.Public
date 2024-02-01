using System.ComponentModel.DataAnnotations.Schema;
using Gradeo.CoreApplication.Domain.Enums;
using Gradeo.CoreApplication.Domain.Interfaces;

namespace Gradeo.CoreApplication.Domain.Entities;

public class PermissionDescriptor : BaseEntity, ISoftDeletable
{
    public int PermissionId { get; set; }
    public string Name { get; set; }
    public bool IsDeleted { get; set; }
    
    [NotMapped]
    public Permission Permission
    {
        get
        {
            if (!Enum.IsDefined(typeof(Permission), PermissionId))
                throw new ArgumentOutOfRangeException("PermissionId is not defined in Permission enum!");

            return (Permission)PermissionId;
        }
        set
        {
            PermissionId = (int)value;
        }
    }
}