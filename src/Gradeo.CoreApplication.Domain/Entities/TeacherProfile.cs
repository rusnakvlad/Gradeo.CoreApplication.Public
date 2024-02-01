using Gradeo.CoreApplication.Domain.Interfaces;

namespace Gradeo.CoreApplication.Domain.Entities;

public class TeacherProfile : BaseEntity, IEntityIdentity, ISoftDeletable
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public bool IsDeleted { get; set; }
    public User User { get; set; }
    public List<TeacherStudyGroup> StudyGroups { get; set; }
    public List<TeacherMasterSubject> AssignedSubjects { get; set; }
}