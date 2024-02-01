using Gradeo.CoreApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gradeo.CoreApplication.Infrastructure.Persistence.Configurations;

public class StudyGroupSubjectConfiguration : IEntityTypeConfiguration<StudyGroupSubject>
{
    public void Configure(EntityTypeBuilder<StudyGroupSubject> builder)
    {
        builder.HasKey(x => new { x.MasterSubjectId, x.StudyGroupId });
    }
}