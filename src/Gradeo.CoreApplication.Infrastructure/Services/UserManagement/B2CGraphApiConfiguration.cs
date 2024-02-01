namespace Gradeo.CoreApplication.Infrastructure.Services.UserManagement;

public class B2CGraphApiConfiguration
{
    public string B2cExtensionAppClientId { get; set; }
    public string B2CIssuer { get; set; }
    public string TenantId { get; set; }
    public string AppId { get; set; }
    public string ClientSecret { get; set; }
    public IEnumerable<string> Scopes { get; set; }
}