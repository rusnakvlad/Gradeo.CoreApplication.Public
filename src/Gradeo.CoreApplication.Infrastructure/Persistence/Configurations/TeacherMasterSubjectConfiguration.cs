using Gradeo.CoreApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gradeo.CoreApplication.Infrastructure.Persistence.Configurations;

public class TeacherMasterSubjectConfiguration : IEntityTypeConfiguration<TeacherMasterSubject>
{
    public void Configure(EntityTypeBuilder<TeacherMasterSubject> builder)
    {
        builder.HasKey(x => new { x.TeacherProfileId, x.MasterSubjectId });
        builder.HasOne(x => x.TeacherProfile).WithMany(x => x.AssignedSubjects).HasForeignKey(x => x.TeacherProfileId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.MasterSubject).WithMany().HasForeignKey(x => x.MasterSubjectId).OnDelete(DeleteBehavior.NoAction);
    }
}