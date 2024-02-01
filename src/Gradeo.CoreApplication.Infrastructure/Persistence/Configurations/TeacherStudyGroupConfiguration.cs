using Gradeo.CoreApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gradeo.CoreApplication.Infrastructure.Persistence.Configurations;

public class TeacherStudyGroupConfiguration : IEntityTypeConfiguration<TeacherStudyGroup>
{
    public void Configure(EntityTypeBuilder<TeacherStudyGroup> builder)
    {
        builder.HasKey(x => new { x.StudyGroupId, x.TeacherProfileId });
        builder.HasOne(x => x.StudyGroup).WithMany(x => x.TeachersForGroup).HasForeignKey(x => x.StudyGroupId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.TeacherProfile).WithMany(x => x.StudyGroups).HasForeignKey(x => x.TeacherProfileId).OnDelete(DeleteBehavior.NoAction);
    }
}