using Gradeo.CoreApplication.Application.Menu.DTOs;
using Gradeo.CoreApplication.Domain.Enums;

namespace Gradeo.CoreApplication.Application.Common.Constants;

public static class MenuConstants
{
    public static List<MenuItemDto> MenuItems => new List<MenuItemDto>()
    {
        new MenuItemDto()
        {
            Name = "Home", RouterLink = "", PrimeIcon = "pi pi-home",
            StudentView = true, TeacherView = true, SchoolAdminView = true, GradeoAdminView = true
        },

        new MenuItemDto()
        {
            Name = "Schools", RouterLink = "schools", PrimeIcon = "pi pi-sitemap",
            RequiredPermission = Permission.CanViewSchools,
            GradeoAdminView = true
        },
        new MenuItemDto()
        {
            Name = "School Profile", RouterLink = "schoolProfile", PrimeIcon = "pi pi-briefcase",
            RequiredPermission = Permission.CanViewSchools,
            SchoolAdminView = true
        },

        new MenuItemDto()
        {
            Name = "Users", RouterLink = "users", PrimeIcon = "pi pi-users",
            RequiredPermission = Permission.CanViewUsers,
            SchoolAdminView = true, GradeoAdminView = true
        },

        new MenuItemDto()
        {
            Name = "Statistics", RouterLink = "statistics", PrimeIcon = "pi pi-chart-bar",
            RequiredPermission = Permission.CanViewStatistics,
            StudentView = true, TeacherView = true, SchoolAdminView = true, GradeoAdminView = true
        },
        new MenuItemDto()
        {
            Name = "Analytics", RouterLink = "analytics", PrimeIcon = "pi pi-chart-pie",
            TeacherView = true, SchoolAdminView = true
        },
        new MenuItemDto()
        {
            Name = "My Profile", RouterLink = "studentProfile", PrimeIcon = "pi pi-user",
            RequiredPermission = Permission.CanViewStudents,
            StudentView = true
        },
        new MenuItemDto()
        {
            Name = "My Profile", RouterLink = "teacherProfile", PrimeIcon = "pi pi-user",
            RequiredPermission = Permission.CanViewTeachers,
            TeacherView = true
        },
        new MenuItemDto()
        {
            Name = "My Grades", RouterLink = "myGrades", PrimeIcon = "pi pi-sort",
            RequiredPermission = Permission.CanViewGrades,
            StudentView = true
        },
        new MenuItemDto()
        {
            Name = "Students Grades", RouterLink = "studentsGrades", PrimeIcon = "pi pi-sort",
            RequiredPermission = Permission.CanViewGrades,
            TeacherView = true, SchoolAdminView = true
        },
        new MenuItemDto()
        {
            Name = "Students", RouterLink = "students", PrimeIcon = "pi pi-id-card",
            RequiredPermission = Permission.CanViewStudents,
            TeacherView = true, SchoolAdminView = true
        },
        new MenuItemDto()
        {
            Name = "Teachers", RouterLink = "teachers", PrimeIcon = "pi pi-id-card",
            RequiredPermission = Permission.CanViewTeachers,
            SchoolAdminView = true
        },
        new MenuItemDto()
        {
            Name = "Admin Data", RouterLink = "admin", PrimeIcon = "pi pi-microsoft",
            RequiredPermission = Permission.CanViewAdminData,
            GradeoAdminView = true, SchoolAdminView = true
        }
    };

    public static List<MenuItemDto> MenuAdminDataItems => new List<MenuItemDto>()
    {
        new MenuItemDto()
        {
            Name = "Subjects", RouterLink = "subjects", PrimeIcon = "pi pi-fw pi-book",
            RequiredPermission = Permission.CanViewMasterSubjects,
            SchoolAdminView = true,
        },
        new MenuItemDto()
        {
            Name = "Roles", RouterLink = "roles", PrimeIcon = "pi pi-fw pi-sitemap",
            RequiredPermission = Permission.CanViewRoles,
            GradeoAdminView = true, SchoolAdminView = true
        },
        new MenuItemDto()
        {
            Name = "Study Groups", RouterLink = "studyGroups", PrimeIcon = "pi pi-fw pi-table",
            RequiredPermission = Permission.CanViewStudyGroups,
            SchoolAdminView = true
        }
    };
}