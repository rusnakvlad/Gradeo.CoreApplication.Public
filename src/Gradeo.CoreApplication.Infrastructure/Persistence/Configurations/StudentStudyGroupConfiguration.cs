using Gradeo.CoreApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gradeo.CoreApplication.Infrastructure.Persistence.Configurations;

public class StudentStudyGroupConfiguration : IEntityTypeConfiguration<StudentStudyGroup>
{
    public void Configure(EntityTypeBuilder<StudentStudyGroup> builder)
    {
        builder.HasKey(x => new { x.StudyGroupId, x.StudentProfileId });
        builder.HasOne(x => x.StudentProfile).WithMany(x => x.StudyGroups).HasForeignKey(x => x.StudentProfileId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.StudyGroup).WithMany(x => x.StudentsInGroup).HasForeignKey(x => x.StudyGroupId).OnDelete(DeleteBehavior.NoAction);
    }
}