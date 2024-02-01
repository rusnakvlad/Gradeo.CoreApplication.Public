using Gradeo.CoreApplication.Application.Common.Extensions;
using Gradeo.CoreApplication.Application.Common.Helpers;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Domain.Entities;
using Gradeo.CoreApplication.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gradeo.CoreApplication.Application.TeacherProfile.UpsertTeacherProfileCommand.Commands;

public class UpsertTeacherProfileCommand : IRequest
{
    public int? Id { get; set; }
    public string UserEmail { get; set; }
    public IEnumerable<int> StudyGroupIds { get; set; }
    public IEnumerable<int> SubjectIds { get; set; }
}

public class UpsertTeacherProfileCommandHandler : IRequestHandler<UpsertTeacherProfileCommand>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ICurrentUserService _currentUserService;
    
    public UpsertTeacherProfileCommandHandler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService)
    {
        _applicationDbContext = applicationDbContext;
        _currentUserService = currentUserService;
    }
    
    public async Task<Unit> Handle(UpsertTeacherProfileCommand request, CancellationToken cancellationToken)
    {
        var userPermissions = _currentUserService.GetCurrentUserPermissions();
        
        if (request.Id.IsNullOrZero())
        {
            PermissionHelper.ValidatePermissions(userPermissions, Permission.CanCreateTeachers);
            var teacherUserId = (await _applicationDbContext.Users.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == request.UserEmail, cancellationToken))?.Id;

            var teacherProfile = new Domain.Entities.TeacherProfile()
            {
                UserId =teacherUserId.Value,
                StudyGroups = request.StudyGroupIds.Select(x => new TeacherStudyGroup(){StudyGroupId = x}).ToList(),
                AssignedSubjects = request.SubjectIds.Select(x => new TeacherMasterSubject() {MasterSubjectId = x}).ToList()
            };

            await _applicationDbContext.TeacherProfiles.AddAsync(teacherProfile, cancellationToken);
        }
        else
        {
            PermissionHelper.ValidatePermissions(userPermissions, Permission.CanEditTeachers);
            var teacher =
                await _applicationDbContext.TeacherProfiles
                    .Include(x => x.StudyGroups)
                    .Include(x => x.AssignedSubjects).FirstOrDefaultAsync(
                    x => x.Id == request.Id,
                    cancellationToken);

            teacher.StudyGroups = request.StudyGroupIds.Select(x => new TeacherStudyGroup() {StudyGroupId = x}).ToList();
            teacher.AssignedSubjects = request.SubjectIds.Select(x => new TeacherMasterSubject() {MasterSubjectId = x}).ToList();
        }
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}
