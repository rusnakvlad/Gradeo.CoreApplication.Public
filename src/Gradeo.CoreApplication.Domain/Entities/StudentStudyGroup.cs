using Gradeo.CoreApplication.Domain.Interfaces;

namespace Gradeo.CoreApplication.Domain.Entities;

public class StudentStudyGroup : BaseEntity
{
    public int StudentProfileId { get; set; }
    public int StudyGroupId { get; set; }
    
    public StudentProfile StudentProfile { get; set; }
    public StudyGroup StudyGroup { get; set; }
}