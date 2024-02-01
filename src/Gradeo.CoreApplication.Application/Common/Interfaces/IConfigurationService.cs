namespace Gradeo.CoreApplication.Application.Common.Interfaces;

public interface IConfigurationService
{
    T GetConfiguration<T>(string key) where T: class;
    Task<T> GetConfigurationAsync<T>(string key, CancellationToken cancellationToken) where T : class;
}