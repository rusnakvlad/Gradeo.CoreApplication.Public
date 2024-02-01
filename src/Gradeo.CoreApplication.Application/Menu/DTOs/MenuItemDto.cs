using Gradeo.CoreApplication.Domain.Enums;

namespace Gradeo.CoreApplication.Application.Menu.DTOs;

public class MenuItemDto
{
    public string Name { get; set; }
    public Permission? RequiredPermission { get; set; }
    public bool StudentView { get; set; }
    public bool TeacherView { get; set; }
    public bool SchoolAdminView { get; set; }
    public bool GradeoAdminView { get; set; }
    public string RouterLink { get; set; }
    public string PrimeIcon { get; set; }
}