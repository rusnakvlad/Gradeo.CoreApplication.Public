using Gradeo.CoreApplication.Application.Common.Extensions;
using Gradeo.CoreApplication.Application.Common.Helpers;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Domain.Entities;
using Gradeo.CoreApplication.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gradeo.CoreApplication.Application.StudyGroups.Commands.UpsertStudyGroupCommand;

public class UpsertStudyGroupCommand : IRequest
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public IEnumerable<int> SubjectIds { get; set; }
}

public class UpsertStudyGroupCommandHandler : IRequestHandler<UpsertStudyGroupCommand>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ICurrentUserService _currentUserService;
    
    public UpsertStudyGroupCommandHandler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService)
    {
        _applicationDbContext = applicationDbContext;
        _currentUserService = currentUserService;
    }
    
    public async Task<Unit> Handle(UpsertStudyGroupCommand request, CancellationToken cancellationToken)
    {
        var currentUserSchoolId = _currentUserService.GetUserBusinessUnitId();

        if (request.Id.IsNullOrZero())
        {
            PermissionHelper.ValidatePermissions(_currentUserService.GetCurrentUserPermissions(), Permission.CanCreateStudyGroups);
            _applicationDbContext.StudyGroups.Add(new StudyGroup()
            {
                Name = request.Name,
                IsActive = request.IsActive,
                BusinessUnitId = currentUserSchoolId.Value,
                StudyGroupSubjects = request.SubjectIds.Select(s => new StudyGroupSubject()
                {
                    MasterSubjectId = s
                }).ToList()
            });
        }
        else
        {
            PermissionHelper.ValidatePermissions(_currentUserService.GetCurrentUserPermissions(), Permission.CanEditStudyGroups);
            var studyGroup = await _applicationDbContext.StudyGroups
                .Include(x => x.StudyGroupSubjects)
                .FirstOrDefaultAsync(x => x.Id == request.Id.Value, cancellationToken);
            if (studyGroup == null)
            {
                throw new ArgumentException($"Study group with id ({request.Id}) was not found");
            }

            studyGroup.Name = request.Name;
            studyGroup.IsActive = request.IsActive;
            studyGroup.StudyGroupSubjects = request.SubjectIds.Select(s => new StudyGroupSubject()
            {
                MasterSubjectId = s
            }).ToList();
        }

        await _applicationDbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}