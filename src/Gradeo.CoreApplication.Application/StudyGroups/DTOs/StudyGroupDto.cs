using Gradeo.CoreApplication.Application.MasterSubjects.DTOs;

namespace Gradeo.CoreApplication.Application.StudyGroups.DTOs;

public class StudyGroupDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public IEnumerable<MasterSubjectDto> Subjects { get; set; }
}