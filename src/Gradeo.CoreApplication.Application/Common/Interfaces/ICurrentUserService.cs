using Gradeo.CoreApplication.Application.Users.DTOs;
using Gradeo.CoreApplication.Domain.Enums;

namespace Gradeo.CoreApplication.Application.Common.Interfaces;

public interface ICurrentUserService
{
    Guid? UserId { get; }
    UserType? UserType { get; }
    UserDetailsDto UserDetails { get; }
    IEnumerable<Permission> GetCurrentUserPermissions();
    int? GetUserBusinessUnitId();
}