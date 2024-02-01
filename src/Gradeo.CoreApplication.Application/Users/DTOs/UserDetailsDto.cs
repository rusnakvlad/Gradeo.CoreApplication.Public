using Gradeo.CoreApplication.Domain.Enums;

namespace Gradeo.CoreApplication.Application.Users.DTOs;

public class UserDetailsDto
{
    public Guid Id { get; set; }
    public UserType? UserType { get; set; }
    public BusinessUnitType SystemType => BusinessUnitId.HasValue ? BusinessUnitType.School : BusinessUnitType.Gradeo;
    public int? BusinessUnitId { get; set; }
    public List<int> Permissions { get; set; }
}