using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Application.Common.Interfaces.Email;
using Gradeo.CoreApplication.Application.Common.Models;
using Gradeo.CoreApplication.Infrastructure.Services.Email.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Gradeo.CoreApplication.Infrastructure.Services.Email;

public class EmailSenderService : IEmailSenderService
{


    private const string ConfigurationKey = "SendGrid";

    private readonly SendGridClient _sendGridClient;
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IConfigurationService _configurationService;
    
    public EmailSenderService(SendGridClient sendGridClient, IApplicationDbContext applicationDbContext, IConfigurationService configurationService)
    {
        _sendGridClient = sendGridClient;
        _applicationDbContext = applicationDbContext;
        _configurationService = configurationService;
    }
    
    public async Task SendWelcomeEmailAsync(B2CUserDetails? userDetails, CancellationToken cancellationToken)
    {
        var config = _configurationService.GetConfiguration<SendGridConfiguration>(ConfigurationKey);
        var emailBody = GetWelcomeEmailBody(userDetails, config, cancellationToken);

        var email = MailHelper.CreateSingleEmail(
            new EmailAddress(config.HostEmail, "My School"),
            new EmailAddress(userDetails.Email),
            "My School Authorization",
            string.Empty,
            emailBody);

        var resp = await _sendGridClient.SendEmailAsync(email, cancellationToken);
    }
    
    private string GetWelcomeEmailBody(
        B2CUserDetails? userDetails,
        SendGridConfiguration config,
        CancellationToken cancellationToken)
    {
        return
            $"Welcome to <b>My School</b>. {userDetails.Email} here is your password: <b>{userDetails.Password}</b>." +
            $"We highly recommend you to change it as soon as possible";
    }
}