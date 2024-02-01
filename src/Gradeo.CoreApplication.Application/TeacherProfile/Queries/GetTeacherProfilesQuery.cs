using AutoMapper;
using AutoMapper.QueryableExtensions;
using Gradeo.CoreApplication.Application.Common.Attributes;
using Gradeo.CoreApplication.Application.Common.Constants;
using Gradeo.CoreApplication.Application.Common.Extensions;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Application.Common.Models;
using Gradeo.CoreApplication.Application.Common.QueryFilters;
using Gradeo.CoreApplication.Application.TeacherProfile.DTOs;
using Gradeo.CoreApplication.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gradeo.CoreApplication.Application.TeacherProfile.Queries;

[Authorize(Permission.CanViewTeachers)]
public class GetTeacherProfilesQuery : IRequest<PaginatedList<TeacherProfileDto>>, IPagination
{
    public int PageNumber { get; set; } = Pagination.DefaultPageNumber;
    public int PageSize { get; set; } = Pagination.DefaultPageSize;
}

public class GetTeacherProfilesQueryHandler : IRequestHandler<GetTeacherProfilesQuery, PaginatedList<TeacherProfileDto>>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    
    public GetTeacherProfilesQueryHandler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService, IMapper mapper)
    {
        _applicationDbContext = applicationDbContext;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }
    
    public async Task<PaginatedList<TeacherProfileDto>> Handle(GetTeacherProfilesQuery request, CancellationToken cancellationToken)
    {
        var currentUserSchoolId = _currentUserService.GetUserBusinessUnitId();

        var teachers = await _applicationDbContext.TeacherProfiles.AsNoTracking()
            .Include(x => x.User)
            .Include(x => x.StudyGroups)
            .Include(x => x.AssignedSubjects)
            .ThenInclude(x => x.MasterSubject)
            .Where(x => x.User.BusinessUnitId == currentUserSchoolId)
            .ProjectTo<TeacherProfileDto>(_mapper.ConfigurationProvider)
            .PaginateListAsync(request.PageNumber, request.PageSize, cancellationToken);

        return teachers;
    }
}