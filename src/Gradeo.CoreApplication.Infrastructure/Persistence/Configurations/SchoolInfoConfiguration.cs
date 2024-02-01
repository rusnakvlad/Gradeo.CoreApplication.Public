using Gradeo.CoreApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gradeo.CoreApplication.Infrastructure.Persistence.Configurations;

public class SchoolInfoConfiguration : IEntityTypeConfiguration<SchoolInfo>
{
    public void Configure(EntityTypeBuilder<SchoolInfo> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.HasOne(x => x.BusinessUnit).WithOne().HasForeignKey<SchoolInfo>(x => x.Id);
    }
}
