using Gradeo.CoreApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gradeo.CoreApplication.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<BusinessUnit> BusinessUnits { get; set; }
    DbSet<Role> Roles { get; set; }
    DbSet<PermissionDescriptor> PermissionDescriptors { get; set; }
    DbSet<RolePermission> RolePermissions { get; set; }
    DbSet<UserRole> UserRoles { get; set; }
    DbSet<MasterSubject> MasterSubjects { get; set; }
    DbSet<SchoolInfo> SchoolsInfo { get; set;}
    DbSet<Domain.Entities.StudentProfile> StudentProfiles { get; set; }
    DbSet<Domain.Entities.TeacherProfile> TeacherProfiles { get; set; }
    DbSet<StudyGroup> StudyGroups { get; set; }
    DbSet<StudyGroupSubject> StudyGroupSubjects { get; set; }
    DbSet<StudentStudyGroup> StudentStudyGroups { get; set; }
    DbSet<TeacherStudyGroup> TeacherStudyGroups { get; set; }
    DbSet<TeacherMasterSubject> TeacherMasterSubjects { get; set; }
    public DbSet<ScheduleShift> ScheduleShifts { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    int SaveChanges();
}
