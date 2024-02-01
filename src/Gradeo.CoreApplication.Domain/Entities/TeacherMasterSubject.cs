using Gradeo.CoreApplication.Domain.Interfaces;

namespace Gradeo.CoreApplication.Domain.Entities;

public class TeacherMasterSubject : BaseEntity
{
    public int TeacherProfileId { get; set; }
    public int MasterSubjectId { get; set; }
    
    public TeacherProfile TeacherProfile { get; set; }
    public MasterSubject MasterSubject { get; set; }
}