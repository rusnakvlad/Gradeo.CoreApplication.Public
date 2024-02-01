using Gradeo.CoreApplication.Application.StudyGroups.DTOs;

namespace Gradeo.CoreApplication.Application.StudentProfile.DTOs;

public class StudentProfileDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public IEnumerable<StudyGroupBasicInfoDto> StudyGroups { get; set; }
}