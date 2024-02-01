using AutoMapper;
using AutoMapper.QueryableExtensions;
using Gradeo.CoreApplication.Application.Common.Attributes;
using Gradeo.CoreApplication.Application.Common.Constants;
using Gradeo.CoreApplication.Application.Common.Extensions;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Application.Common.Models;
using Gradeo.CoreApplication.Application.Common.QueryFilters;
using Gradeo.CoreApplication.Application.StudentProfile.DTOs;
using Gradeo.CoreApplication.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gradeo.CoreApplication.Application.StudentProfile.Queries;

[Authorize(Permission.CanViewStudents)]
public class GetStudentProfilesQuery : IRequest<PaginatedList<StudentProfileDto>>, IPagination
{
    public int PageNumber { get; set; } = Pagination.DefaultPageNumber;
    public int PageSize { get; set; } = Pagination.DefaultPageSize;
}

public class GetStudentProfilesQueryHandler : IRequestHandler<GetStudentProfilesQuery, PaginatedList<StudentProfileDto>>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    
    public GetStudentProfilesQueryHandler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService, IMapper mapper)
    {
        _applicationDbContext = applicationDbContext;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }
    
    public async Task<PaginatedList<StudentProfileDto>> Handle(GetStudentProfilesQuery request, CancellationToken cancellationToken)
    {
        var currentUserSchoolId = _currentUserService.GetUserBusinessUnitId();

        var students = await _applicationDbContext.StudentProfiles.AsNoTracking()
            .Include(x => x.User)
            .Include(x => x.StudyGroups)
            .Where(x => x.User.BusinessUnitId == currentUserSchoolId)
            .ProjectTo<StudentProfileDto>(_mapper.ConfigurationProvider)
            .PaginateListAsync(request.PageNumber, request.PageSize, cancellationToken);

        return students;
    }
}
