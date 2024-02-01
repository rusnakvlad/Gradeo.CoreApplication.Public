using Gradeo.CoreApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gradeo.CoreApplication.Infrastructure.Persistence.Seed;

public class SystemDefaultUsersSeed
{
    public const string GradeoSystemAdminUserGuid = "a004cf46-45db-4aa4-92f9-8215ffce3f4f";
    
    public static void Seed(ModelBuilder builder)
    {
        var userId = new Guid(GradeoSystemAdminUserGuid);
        var gradeoSystemAdmin = new User()
        {
            Id = userId,
            FirstName = "Gradeo",
            LastName = "Admin",
            Email = "gradeoadmin@gradeo.com"
        };

        builder.Entity<User>().HasData(gradeoSystemAdmin);
        builder.Entity<UserRole>().HasData(new UserRole() { RoleId = SystemDefinedRolesSeed.GradeoAdminRoleId, UserId = userId});
    }
}