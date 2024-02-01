using Gradeo.CoreApplication.Application.Common.Attributes;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Application.Schools.DTOs;
using Gradeo.CoreApplication.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gradeo.CoreApplication.Application.Schools.Queries;

[Authorize(Permission.CanViewSchools)]
public class GetSchoolProfileByIdQuery : IRequest<SchoolProfileDto>
{
    public int? schoolId { get; set; }
}

public class GetSchoolProfileByIdQueryHandler : IRequestHandler<GetSchoolProfileByIdQuery, SchoolProfileDto>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IApplicationDbContext _applicationDbContext;
    
    public GetSchoolProfileByIdQueryHandler(ICurrentUserService currentUserService, IApplicationDbContext applicationDbContext)
    {
        _currentUserService = currentUserService;
        _applicationDbContext = applicationDbContext;
    }
    
    public async Task<SchoolProfileDto> Handle(GetSchoolProfileByIdQuery request, CancellationToken cancellationToken)
    {
        var currentUserSchoolId = _currentUserService.GetUserBusinessUnitId();
        int? schoolId = currentUserSchoolId ?? request.schoolId;
        var schoolInfo = await _applicationDbContext.SchoolsInfo.AsNoTracking()
            .Include(x => x.BusinessUnit)
            .FirstOrDefaultAsync(x => x.Id == schoolId, cancellationToken);
        var teachersCount = await _applicationDbContext.TeacherProfiles.Include(x => x.User)
            .Where(x => x.User.BusinessUnitId == schoolId).CountAsync(cancellationToken);
        var studentsCount = await _applicationDbContext.StudentProfiles.Include(x => x.User)
            .Where(x => x.User.BusinessUnitId == schoolId).CountAsync(cancellationToken);
        return new SchoolProfileDto()
        {
            Id = schoolInfo.Id,
            Name = schoolInfo.BusinessUnit.Name,
            Country = schoolInfo.Country,
            City = schoolInfo.City,
            TeachersCount = teachersCount,
            StudentsCount = studentsCount
        };
    }
}