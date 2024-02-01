using AutoMapper;
using AutoMapper.QueryableExtensions;
using Gradeo.CoreApplication.Application.Common.Attributes;
using Gradeo.CoreApplication.Application.Common.Extensions;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Application.StudyGroups.DTOs;
using Gradeo.CoreApplication.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gradeo.CoreApplication.Application.StudyGroups.Queries;

[Authorize(Permission.CanViewStudyGroups)]
public class GetAllActiveStudyGroupsQuery : IRequest<IEnumerable<StudyGroupBasicInfoDto>>
{
    
}

public class GetAllActiveStudyGroupsQueryHandler : IRequestHandler<GetAllActiveStudyGroupsQuery, IEnumerable<StudyGroupBasicInfoDto>>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    
    public GetAllActiveStudyGroupsQueryHandler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService, IMapper mapper)
    {
        _applicationDbContext = applicationDbContext;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<StudyGroupBasicInfoDto>> Handle(GetAllActiveStudyGroupsQuery request, CancellationToken cancellationToken)
    {
        var currentUser = _currentUserService.UserDetails;
        var teacherStudyGroupIds = new List<int>();
        if (currentUser.UserType is UserType.Teacher)
        {
            teacherStudyGroupIds = await _applicationDbContext.TeacherStudyGroups.AsNoTracking()
                .Include(x => x.TeacherProfile)
                .Where(x => x.TeacherProfile.UserId == currentUser.Id).Select(x => x.StudyGroupId)
                .ToListAsync(cancellationToken);
            
        }
        return await _applicationDbContext.StudyGroups.AsNoTracking()
            .Where(x => x.IsActive)
            .Where(x => x.BusinessUnitId == currentUser.BusinessUnitId)
            .WhereIf(currentUser.UserType is UserType.Teacher, x => teacherStudyGroupIds.Contains(x.Id))
            .Select(x => new StudyGroupBasicInfoDto(){Id = x.Id, Name = x.Name})
            .ToListAsync(cancellationToken);
    }
}