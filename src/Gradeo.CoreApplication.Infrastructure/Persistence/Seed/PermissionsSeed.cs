using Gradeo.CoreApplication.Domain.Entities;
using Gradeo.CoreApplication.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Gradeo.CoreApplication.Infrastructure.Persistence.Seed;

public class PermissionsSeed
{
    public static void Seed(ModelBuilder builder)
    {
        var permissions = Enum.GetValues<Permission>().Select(x => new PermissionDescriptor()
        {
            PermissionId = (int)x,
            Name = x.ToString()
        });
        builder.Entity<PermissionDescriptor>().HasData(permissions);
    }
}