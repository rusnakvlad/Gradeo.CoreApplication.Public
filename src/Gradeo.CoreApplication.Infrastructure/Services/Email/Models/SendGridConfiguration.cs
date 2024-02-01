namespace Gradeo.CoreApplication.Infrastructure.Services.Email.Models;

public class SendGridConfiguration
{
    public string APIKey { get; set; }
    public string ApplicationUrl { get; set; }
    public string HostEmail { get; set; }
}