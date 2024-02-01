using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Application.Common.Models;
using Microsoft.Graph;

namespace Gradeo.CoreApplication.Infrastructure.Services.UserManagement;

public class B2CManagementService : IB2CManagementService
{
    private const string ConfigurationKey = "GraphAPI";
    private const int PasswordLength = 15;
    
    private readonly GraphServiceClient _graphClient;
    private readonly IConfigurationService _configurationService;
    
    public B2CManagementService(IConfigurationService configurationService, GraphServiceClient graphClient)
    {
        _graphClient = graphClient;
        _configurationService = configurationService;
    }
    
    public async Task<B2CUserDetails?> AddUserAsync(Domain.Entities.User newUser, CancellationToken cancellationToken)
    {
        var user = await GetUserByEmail(newUser.Email, cancellationToken);
        
        if (user is not null)
        {
            var error = new ArgumentException($"User with email {newUser.Email} already exists in B2C");
            throw error;
        }
                
        var config = await _configurationService.GetConfigurationAsync<B2CGraphApiConfiguration>(ConfigurationKey, cancellationToken);
        var newPassword = B2CPasswordHelper.GenerateNewPassword(PasswordLength);
        
        try
        {
            var result = await _graphClient.Users
                .Request()
                .AddAsync(new User
                {
                    AccountEnabled = !newUser.IsDeleted,
                    GivenName = newUser.FirstName,
                    Surname = newUser.LastName,                   
                    DisplayName = $"{newUser.FirstName} {newUser.LastName}",
                    Mail = newUser.Email,
                    Identities = new List<ObjectIdentity>
                    {
                        new ()
                        {
                            SignInType = "emailAddress",
                            Issuer = config.B2CIssuer,
                            IssuerAssignedId = newUser.Email
                        }
                    },
                    PasswordProfile = new PasswordProfile
                    {
                        Password = newPassword,
                        ForceChangePasswordNextSignIn = false,
                    },
                    PasswordPolicies = "DisablePasswordExpiration"
                }, cancellationToken);

            return CreateB2CUserDetails(result, newPassword);
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public async Task UpdateUserAsync(Domain.Entities.User userInfo, CancellationToken cancellationToken)
    {
        var user = await GetUserByEmail(userInfo.Email, cancellationToken);
        
        try
        {
            if (user is not null)
            {
                var newUserInfo = new User
                {
                    AccountEnabled = !userInfo.IsDeleted,
                    GivenName = userInfo.FirstName,
                    Surname = userInfo.LastName
                };

                await _graphClient.Users[user.Id].Request().UpdateAsync(newUserInfo, cancellationToken);
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public Task<B2CUserDetails?> ResetUserPassword(string email, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    
    private async Task<B2CUserDetails?> GetUserByEmail(string email, CancellationToken cancellationToken)
    {
        var config = await _configurationService.GetConfigurationAsync<B2CGraphApiConfiguration>(ConfigurationKey, cancellationToken);
    
        var user = await _graphClient.Users
            .Request()
            .Filter($"identities/any(c:c/issuerAssignedId eq '{email}' and c/issuer eq '{config.B2CIssuer}')")
            .Select("Id,Mail,GivenName,Surname,DisplayName,UserPrincipalName,Identities")
            .GetAsync(cancellationToken);

        return CreateB2CUserDetails(user?.CurrentPage.FirstOrDefault(), string.Empty);
    }
    
    private static B2CUserDetails? CreateB2CUserDetails(User? user, string userPassword)
    {
        return user is not null ? 
            new B2CUserDetails 
            {
                Id = user.Id,
                GivenName = user.GivenName,
                Surname = user.Surname,
                Email = user.Mail,
                Password = userPassword
            } : 
            null;
    }
}