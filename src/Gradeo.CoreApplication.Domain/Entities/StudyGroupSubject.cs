using Gradeo.CoreApplication.Domain.Interfaces;

namespace Gradeo.CoreApplication.Domain.Entities;

public class StudyGroupSubject : BaseEntity
{
    public int MasterSubjectId { get; set; }
    public int StudyGroupId { get; set; }
    public StudyGroup StudyGroup { get; set; }
    public MasterSubject MasterSubject { get; set; }
}