namespace Gradeo.CoreApplication.Application.Grades.DTOs;

public class StudentWithGradesDto
{
    public int StudentId { get; set; }
    public string StudentFirstName { get; set; }
    public string StudentLastName { get; set; }
    public IEnumerable<GradeDto> Grades { get; set; }
}