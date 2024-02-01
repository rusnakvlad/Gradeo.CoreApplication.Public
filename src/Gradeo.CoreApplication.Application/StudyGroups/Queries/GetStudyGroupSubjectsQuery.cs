using System.Collections;
using Gradeo.CoreApplication.Application.Common.Attributes;
using Gradeo.CoreApplication.Application.Common.Extensions;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Application.MasterSubjects.DTOs;
using Gradeo.CoreApplication.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gradeo.CoreApplication.Application.StudyGroups.Queries;

public class GetStudyGroupSubjectsQuery : IRequest<IEnumerable<MasterSubjectDto>>
{
    public int GroupId { get; set; }
}

public class GetStudyGroupSubjectsQueryHandler : IRequestHandler<GetStudyGroupSubjectsQuery, IEnumerable<MasterSubjectDto>>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ICurrentUserService _currentUserService;
    
    public GetStudyGroupSubjectsQueryHandler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService)
    {
        _applicationDbContext = applicationDbContext;
        _currentUserService = currentUserService;
    }
    
    public async Task<IEnumerable<MasterSubjectDto>> Handle(GetStudyGroupSubjectsQuery request, CancellationToken cancellationToken)
    {
        var currentUser = _currentUserService.UserDetails;
        var teacherAssignedSubjectIds = new List<int>();
        if (currentUser.UserType is UserType.Teacher)
        {
            teacherAssignedSubjectIds = await _applicationDbContext.TeacherMasterSubjects.AsNoTracking()
                .Include(x => x.TeacherProfile)
                .Where(x => x.TeacherProfile.UserId == currentUser.Id).Select(x => x.MasterSubjectId)
                .ToListAsync(cancellationToken);
            
        }
        return await _applicationDbContext.StudyGroups.AsNoTracking()
            .Include(x => x.StudyGroupSubjects)
            .ThenInclude(x => x.MasterSubject)
            .Where(x => x.Id == request.GroupId)
            .SelectMany(x => x.StudyGroupSubjects.Select(s => new MasterSubjectDto()
                { Id = s.MasterSubjectId, Name = s.MasterSubject.Name }))
            .WhereIf(currentUser.UserType is UserType.Teacher, x => teacherAssignedSubjectIds.Contains(x.Id))
            .ToListAsync(cancellationToken);
    }
}