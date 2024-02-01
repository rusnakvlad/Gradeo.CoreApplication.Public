using Gradeo.CoreApplication.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Gradeo.CoreApplication.Infrastructure.Services;

public class ConfigurationService : IConfigurationService
{
    private readonly IConfiguration _configuration;
    public ConfigurationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<T> GetConfigurationAsync<T>(string key, CancellationToken cancellationToken) where T: class
    {
        return await Task.FromResult(GetConfiguration<T>(key));
    }
    
    public T GetConfiguration<T>(string key) where T: class
    {
        var config = _configuration.GetSection(key).Get<T>();

        return config;
    }
}