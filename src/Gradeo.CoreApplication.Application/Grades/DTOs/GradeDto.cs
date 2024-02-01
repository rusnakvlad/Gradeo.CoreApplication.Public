using Newtonsoft.Json;

namespace Gradeo.CoreApplication.Application.Grades.DTOs;

public class GradeDto
{
    public string? Id { get; set; }
    public decimal Grade { get; set; }
    public int SubjectId { get; set; }
    public string SubjectName { get; set; }
    public int StudentId { get; set; }
    public DateTimeOffset Date { get; set; }
}