using PasswordGenerator;

namespace Gradeo.CoreApplication.Infrastructure.Services.UserManagement;

public class B2CPasswordHelper
{
    public static string GenerateNewPassword(int length)
    {
        var password = new Password(
            includeLowercase: true,
            includeUppercase: true, 
            includeNumeric: true, 
            includeSpecial: true,
            passwordLength: 24);

        return password.Next();
    }
}