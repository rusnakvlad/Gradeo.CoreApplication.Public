using Gradeo.CoreApplication.Application.MasterSubjects.DTOs;
using Gradeo.CoreApplication.Application.StudyGroups.DTOs;

namespace Gradeo.CoreApplication.Application.TeacherProfile.DTOs;

public class TeacherProfileDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public IEnumerable<StudyGroupBasicInfoDto> StudyGroups { get; set; }
    public IEnumerable<MasterSubjectDto> Subjects { get; set; }
}