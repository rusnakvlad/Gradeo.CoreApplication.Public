using Gradeo.CoreApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gradeo.CoreApplication.Infrastructure.Persistence.Configurations;

public class PermissionDescriptorConfiguration : IEntityTypeConfiguration<PermissionDescriptor>
{
    public void Configure(EntityTypeBuilder<PermissionDescriptor> builder)
    {
        builder.HasKey(x => x.PermissionId);
        builder.Property(x => x.PermissionId).ValueGeneratedNever();
    }
}