using Gradeo.CoreApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gradeo.CoreApplication.Infrastructure.Persistence.Configurations;

public class StudyGroupConfiguration : IEntityTypeConfiguration<StudyGroup>
{
    public void Configure(EntityTypeBuilder<StudyGroup> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.BusinessUnit).WithMany().HasForeignKey(x => x.BusinessUnitId);
        builder.HasMany(x => x.StudyGroupSubjects).WithOne(x => x.StudyGroup).HasForeignKey(x => x.StudyGroupId).OnDelete(DeleteBehavior.NoAction);
    }
}