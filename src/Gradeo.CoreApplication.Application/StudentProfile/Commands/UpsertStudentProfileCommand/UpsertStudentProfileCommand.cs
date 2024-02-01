using Gradeo.CoreApplication.Application.Common.Extensions;
using Gradeo.CoreApplication.Application.Common.Helpers;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Domain.Entities;
using Gradeo.CoreApplication.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gradeo.CoreApplication.Application.StudentProfile.Commands.UpsertStudentProfileCommand;

public class UpsertStudentProfileCommand : IRequest
{
    public int? Id { get; set; }
    public string UserEmail { get; set; }
    public int StudyGroupId { get; set; }
}

public class UpsertStudentProfileCommandHandler : IRequestHandler<UpsertStudentProfileCommand>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ICurrentUserService _currentUserService;
    
    public UpsertStudentProfileCommandHandler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService)
    {
        _applicationDbContext = applicationDbContext;
        _currentUserService = currentUserService;
    }
    
    public async Task<Unit> Handle(UpsertStudentProfileCommand request, CancellationToken cancellationToken)
    {
        if (request.Id.IsNullOrZero())
        {
            PermissionHelper.ValidatePermissions(_currentUserService.GetCurrentUserPermissions(), Permission.CanCreateStudents);
            var studentUserId = (await _applicationDbContext.Users.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == request.UserEmail, cancellationToken))?.Id;

            var studentProfile = new Domain.Entities.StudentProfile()
            {
                UserId = studentUserId.Value,
                StudyGroups = new List<StudentStudyGroup>()
                {
                    new StudentStudyGroup()
                    {
                        StudyGroupId = request.StudyGroupId
                    }
                }
            };

            await _applicationDbContext.StudentProfiles.AddAsync(studentProfile, cancellationToken);
        }
        else
        {
            PermissionHelper.ValidatePermissions(_currentUserService.GetCurrentUserPermissions(), Permission.CanEditStudents);
            var student =
                await _applicationDbContext.StudentProfiles.Include(x => x.StudyGroups).FirstOrDefaultAsync(
                    x => x.Id == request.Id,
                    cancellationToken);

            student.StudyGroups = new List<StudentStudyGroup>()
            {
                new StudentStudyGroup()
                {
                    StudyGroupId = request.StudyGroupId
                }
            };
        }
        
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}