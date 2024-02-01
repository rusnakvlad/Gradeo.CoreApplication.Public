using Gradeo.CoreApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gradeo.CoreApplication.Infrastructure.Persistence.Configurations;

public class ScheduleShiftConfiguration : IEntityTypeConfiguration<ScheduleShift>
{
    public void Configure(EntityTypeBuilder<ScheduleShift> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.BusinessUnit).WithMany().HasForeignKey(x => x.BusinessUnitId);
    }
}