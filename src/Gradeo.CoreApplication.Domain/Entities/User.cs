using Gradeo.CoreApplication.Domain.Enums;
using Gradeo.CoreApplication.Domain.Interfaces;

namespace Gradeo.CoreApplication.Domain.Entities;

public class User : BaseEntity, ISoftDeletable
{
    public Guid Id { get; set; }
    public int? BusinessUnitId { get; set; }
    public UserType? UserType { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool IsDeleted { get; set; }
    public List<UserRole> UserRoles { get; set; }
    
    public BusinessUnit? BusinessUnit { get; set; }
}