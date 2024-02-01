namespace Gradeo.CoreApplication.Application.Schools.DTOs;

public class SchoolProfileDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    
    public int StudentsCount { get; set; }
    public int TeachersCount { get; set; }
}