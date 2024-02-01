using Gradeo.CoreApplication.Application.Common.Models;

namespace Gradeo.CoreApplication.Application.Common.Interfaces.Email;

public interface IEmailSenderService
{
    Task SendWelcomeEmailAsync(B2CUserDetails? userDetails, CancellationToken cancellationToken);
}