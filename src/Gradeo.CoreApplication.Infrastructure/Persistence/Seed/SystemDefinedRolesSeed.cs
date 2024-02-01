using Gradeo.CoreApplication.Domain.Entities;
using Gradeo.CoreApplication.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Gradeo.CoreApplication.Infrastructure.Persistence.Seed;

public class SystemDefinedRolesSeed
{
    public const int GradeoAdminRoleId = 1;
    public const int SchoolAdminRoleId = 2;
    public const int TeacherRoleId = 3;
    public const int StudentRoleId = 4;
    public static void Seed(ModelBuilder builder)
    {
        
        var gradeoSystemAdmin = new Role()
        {
            Id = GradeoAdminRoleId,
            RoleName = "Gradeo System Admin",
            BusinessUnitType = BusinessUnitType.Gradeo,
            IsDefault = true
        };
        var schoolAdmin = new Role()
        {
            Id = SchoolAdminRoleId,
            RoleName = "School System Admin",
            BusinessUnitType = BusinessUnitType.School,
            IsDefault = true
        };
        var teacher = new Role()
        {
            Id = TeacherRoleId,
            RoleName = "Teacher",
            BusinessUnitType = BusinessUnitType.School,
            IsDefault = true
        };
        var student = new Role()
        {
            Id = StudentRoleId,
            RoleName = "Student",
            BusinessUnitType = BusinessUnitType.School,
            IsDefault = true
        };

        var gradeoAdminPermissions = GetGradeoAdminPermissions().Select(x => new RolePermission()
        {
            RoleId = GradeoAdminRoleId,
            PermissionId = (int)x
        }).ToList();
        var schoolAdminPermissions = GetSchoolAdminPermissions().Select(x => new RolePermission()
        {
            RoleId = SchoolAdminRoleId,
            PermissionId = (int)x
        }).ToList();
        var teacherPermissions = GetTeacherPermissions().Select(x => new RolePermission()
        {
            RoleId = TeacherRoleId,
            PermissionId = (int)x
        }).ToList();
        var studentPermissions = GetStudentPermissions().Select(x => new RolePermission()
        {
            RoleId = StudentRoleId,
            PermissionId = (int)x
        }).ToList();
        
        builder.Entity<Role>().HasData(gradeoSystemAdmin, schoolAdmin, teacher, student);
        builder.Entity<RolePermission>().HasData(gradeoAdminPermissions);
        builder.Entity<RolePermission>().HasData(schoolAdminPermissions);
        builder.Entity<RolePermission>().HasData(teacherPermissions);
        builder.Entity<RolePermission>().HasData(studentPermissions);
    }

    public static IEnumerable<Permission> GetGradeoAdminPermissions()
    {
        return new List<Permission>()
        {
            Permission.CanViewSchools,
            Permission.CanCreateSchools,
            Permission.CanEditSchools,
            Permission.CanDeleteSchools,
            
            Permission.CanViewAdminData,
            
            Permission.CanViewUsers,
            Permission.CanCreateUsers,
            Permission.CanDeleteUsers,
            Permission.CanEditUsers,
            
            Permission.CanViewMasterSubjects,
            Permission.CanCreateMasterSubjects,
            Permission.CanEditMasterSubjects,
            Permission.CanDeleteMasterSubjects,
            
            Permission.CanViewRoles,
            Permission.CanCreateRoles,
            Permission.CanEditRoles,
            Permission.CanDeleteRoles,
        };
    }

    public static IEnumerable<Permission> GetSchoolAdminPermissions()
    {
        return new List<Permission>()
        {
            Permission.CanViewSchools,
            
            Permission.CanViewTeachers,
            Permission.CanCreateTeachers,
            Permission.CanEditTeachers,
            Permission.CanDeleteTeachers,
            
            Permission.CanViewStudents,
            Permission.CanCreateStudents,
            Permission.CanEditStudents,
            Permission.CanDeleteStudents,
            
            Permission.CanViewAdminData,
            
            Permission.CanViewUsers,
            Permission.CanCreateUsers,
            Permission.CanEditUsers,
            Permission.CanDeleteUsers,
            
            Permission.CanViewGrades,
            Permission.CanCreateGrades,
            Permission.CanEditGrades,
            Permission.CanDeleteGrades,
            
            Permission.CanViewMasterSubjects,
            Permission.CanCreateMasterSubjects,
            Permission.CanEditMasterSubjects,
            Permission.CanDeleteMasterSubjects,
            
            Permission.CanViewRoles,
            Permission.CanCreateRoles,
            Permission.CanEditRoles,
            Permission.CanDeleteRoles,
            
            Permission.CanViewStudyGroups,
            Permission.CanCreateStudyGroups,
            Permission.CanEditStudyGroups,
            Permission.CanDeleteStudyGroups,
            
            Permission.CanViewStatistics
        };
    }
    
    public static IEnumerable<Permission> GetTeacherPermissions()
    {
        return new List<Permission>()
        {
            Permission.CanViewTeachers,
            Permission.CanViewStudents,
            Permission.CanViewGrades,
            Permission.CanCreateGrades,
            Permission.CanEditGrades,
            Permission.CanDeleteGrades,
            Permission.CanViewStudyGroups,
            Permission.CanViewMasterSubjects,
        };
    }
    
    public static IEnumerable<Permission> GetStudentPermissions()
    {
        return new List<Permission>()
        {
            Permission.CanViewStudents,
            Permission.CanViewGrades
        };
    }

    public static IEnumerable<RolePermission> MapAndGetPermissions(IEnumerable<Permission> permissions, int roleId)
    {
        return permissions.Select(x => new RolePermission()
        {
            RoleId = roleId,
            PermissionId = (int)x
        });
    }
}