using Gradeo.CoreApplication.Domain.Interfaces;

namespace Gradeo.CoreApplication.Domain.Entities;

public class StudentProfile : BaseEntity, IEntityIdentity, ISoftDeletable
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public bool IsDeleted { get; set; }
    public User User { get; set; }
    public List<StudentStudyGroup> StudyGroups { get; set; }
}
