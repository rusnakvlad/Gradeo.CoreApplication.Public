namespace Gradeo.CoreApplication.Domain.Entities;

public class UserRole : BaseEntity
{
    public Guid UserId { get; set; }
    public int RoleId { get; set; }
    
    public User User { get; set; }
    public Role Role { get; set; }
}