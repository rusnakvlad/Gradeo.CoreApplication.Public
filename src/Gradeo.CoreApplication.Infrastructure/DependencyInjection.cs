using Azure.Identity;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Application.Common.Interfaces.Email;
using Gradeo.CoreApplication.Infrastructure.Persistence;
using Gradeo.CoreApplication.Infrastructure.Services;
using Gradeo.CoreApplication.Infrastructure.Services.Email;
using Gradeo.CoreApplication.Infrastructure.Services.Email.Models;
using Gradeo.CoreApplication.Infrastructure.Services.UserManagement;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;
using SendGrid;

namespace Gradeo.CoreApplication.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IConfigurationService, ConfigurationService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddStackExchangeRedisCache(options =>
        {
            var connectionString = configuration.GetConnectionString("RedisCache");
            options.Configuration = connectionString;
        });

        services.AddTransient<ICacheService, CacheService>();
        
        services.AddScoped<IEmailSenderService, EmailSenderService>(options =>
        {
            var configurationService = options.GetService<IConfigurationService>();
            var applicationDbContext = options.GetService<IApplicationDbContext>();
            var b2CConfig = configurationService?.GetConfiguration<SendGridConfiguration>("SendGrid")!;

            return new EmailSenderService(new SendGridClient(b2CConfig.APIKey), applicationDbContext!, configurationService!);
        });
        services.AddScoped<IB2CManagementService, B2CManagementService>(options =>
        {
            var configurationService = options.GetService<IConfigurationService>();
            var b2CConfig = configurationService?.GetConfiguration<B2CGraphApiConfiguration>("GraphAPI")!;

            var graphClient = new GraphServiceClient(
                new ClientSecretCredential(b2CConfig.TenantId, b2CConfig.AppId, b2CConfig.ClientSecret),
                b2CConfig.Scopes);

            return new B2CManagementService(configurationService!, graphClient);
        });
        
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("CoreApplicationDb");
                options.UseSqlServer(connectionString);
                options.EnableSensitiveDataLogging();
            }
        );
        services.AddSingleton<ICosmosDbContext, CosmosDbContext>(options =>
        {
            var cosmosDbConnectionString = configuration.GetConnectionString("CosmosDb");
            var containersToInitialize = new List<(string, string)> { ("GradeoSharedDB", "Grades") };
            var cosmosClient = CosmosClient.CreateAndInitializeAsync( cosmosDbConnectionString, containersToInitialize).Result;
            var database = cosmosClient.GetDatabase("GradeoSharedDB");
            return new CosmosDbContext(database);
        });
        
        return services;
    }
}