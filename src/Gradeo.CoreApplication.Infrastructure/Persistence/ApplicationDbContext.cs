using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Domain.Entities;
using Gradeo.CoreApplication.Infrastructure.Persistence.Configurations;
using Gradeo.CoreApplication.Infrastructure.Persistence.Seed;
using Microsoft.EntityFrameworkCore;

namespace Gradeo.CoreApplication.Infrastructure.Persistence;

public class ApplicationDbContext : BaseDbContext<ApplicationDbContext>, IApplicationDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<BusinessUnit> BusinessUnits { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<PermissionDescriptor> PermissionDescriptors { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<MasterSubject> MasterSubjects { get; set; }
    public DbSet<SchoolInfo> SchoolsInfo { get; set; }
    public DbSet<StudentProfile> StudentProfiles { get; set; }
    public DbSet<TeacherProfile> TeacherProfiles { get; set; }
    public DbSet<StudyGroup> StudyGroups { get; set; }
    public DbSet<StudyGroupSubject> StudyGroupSubjects { get; set; }
    public DbSet<StudentStudyGroup> StudentStudyGroups { get; set; }
    public DbSet<TeacherStudyGroup> TeacherStudyGroups { get; set; }
    public DbSet<TeacherMasterSubject> TeacherMasterSubjects { get; set; }
    public DbSet<ScheduleShift> ScheduleShifts { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new BusinessUnitConfiguration());
        builder.ApplyConfiguration(new RoleConfiguration());
        builder.ApplyConfiguration(new PermissionDescriptorConfiguration());
        builder.ApplyConfiguration(new RolePermissionConfiguration());
        builder.ApplyConfiguration(new UserRoleConfiguration());
        builder.ApplyConfiguration(new MasterSubjectConfiguration());
        builder.ApplyConfiguration(new SchoolInfoConfiguration());
        builder.ApplyConfiguration(new StudentProfileConfiguration());
        builder.ApplyConfiguration(new TeacherProfileConfiguration());
        builder.ApplyConfiguration(new StudyGroupConfiguration());
        builder.ApplyConfiguration(new StudyGroupSubjectConfiguration());
        builder.ApplyConfiguration(new StudentStudyGroupConfiguration());
        builder.ApplyConfiguration(new TeacherStudyGroupConfiguration());
        builder.ApplyConfiguration(new TeacherMasterSubjectConfiguration());
        builder.ApplyConfiguration(new ScheduleShiftConfiguration());
        
        ApplyQueryFilters<User>(builder);
        ApplyQueryFilters<BusinessUnit>(builder);
        ApplyQueryFilters<Role>(builder);
        ApplyQueryFilters<PermissionDescriptor>(builder);
        ApplyQueryFilters<RolePermission>(builder);
        ApplyQueryFilters<UserRole>(builder);
        ApplyQueryFilters<MasterSubject>(builder);
        ApplyQueryFilters<SchoolInfo>(builder);
        ApplyQueryFilters<StudentProfile>(builder);
        ApplyQueryFilters<TeacherProfile>(builder);
        ApplyQueryFilters<StudyGroup>(builder);
        ApplyQueryFilters<StudyGroupSubject>(builder);
        ApplyQueryFilters<StudentStudyGroup>(builder);
        ApplyQueryFilters<TeacherStudyGroup>(builder);
        ApplyQueryFilters<TeacherMasterSubject>(builder);
        ApplyQueryFilters<ScheduleShiftConfiguration>(builder);
        
        PermissionsSeed.Seed(builder);
        SystemDefinedRolesSeed.Seed(builder);
        SystemDefaultUsersSeed.Seed(builder);
        
        base.OnModelCreating(builder);
    }
}