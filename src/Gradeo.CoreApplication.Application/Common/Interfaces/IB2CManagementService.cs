using Gradeo.CoreApplication.Application.Common.Models;
using Gradeo.CoreApplication.Domain.Entities;

namespace Gradeo.CoreApplication.Application.Common.Interfaces;

public interface IB2CManagementService
{
    Task<B2CUserDetails?> AddUserAsync(User newUser, CancellationToken cancellationToken);
    Task UpdateUserAsync(Domain.Entities.User userInfo, CancellationToken cancellationToken);
    Task<B2CUserDetails?> ResetUserPassword(string email, CancellationToken cancellationToken);
}