using Gradeo.CoreApplication.Domain.Interfaces;

namespace Gradeo.CoreApplication.Domain.Entities;

public class TeacherStudyGroup : BaseEntity
{
    public int StudyGroupId { get; set; }
    public int TeacherProfileId { get; set; }
    
    public TeacherProfile TeacherProfile { get; set; }
    public StudyGroup StudyGroup { get; set; }
}