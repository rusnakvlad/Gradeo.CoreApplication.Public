using Gradeo.CoreApplication.Domain.Interfaces;

namespace Gradeo.CoreApplication.Domain.Entities;

public class StudyGroup : BaseEntity, IEntityIdentity, IBusinessUnitBased, ISoftDeletable
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public int BusinessUnitId { get; set; }
    public BusinessUnit BusinessUnit { get; set; }
    public bool IsDeleted { get; set; }
    public List<StudyGroupSubject> StudyGroupSubjects { get; set; }
    public List<StudentStudyGroup> StudentsInGroup { get; set; }
    public List<TeacherStudyGroup> TeachersForGroup { get; set; }
}