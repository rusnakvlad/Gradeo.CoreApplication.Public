using Gradeo.CoreApplication.Domain.Enums;

namespace Gradeo.CoreApplication.Application.Users.DTOs;

public class UserDto
{
    public Guid? Id { get; set; }
    public string? UserType { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public List<int>? RoleIds { get; set; }
}