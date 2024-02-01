using AutoMapper;
using AutoMapper.QueryableExtensions;
using Gradeo.CoreApplication.Application.Common.Attributes;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Application.StudentProfile.DTOs;
using Gradeo.CoreApplication.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gradeo.CoreApplication.Application.StudentProfile.Queries;

[Authorize(Permission.CanViewStudents)]
public class GetStudentProfileByIdQuery : IRequest<StudentProfileDto>
{
    public int? Id { get; set; }
}

public class GetStudentProfileByIdQueryHandler : IRequestHandler<GetStudentProfileByIdQuery, StudentProfileDto>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;
    
    public GetStudentProfileByIdQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper, ICurrentUserService currentUserService)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }
    
    public async Task<StudentProfileDto> Handle(GetStudentProfileByIdQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        return await _applicationDbContext.StudentProfiles.AsNoTracking()
            .Include(x => x.User)
            .Include(x => x.StudyGroups)
            .Where(x => x.UserId == userId)
            .ProjectTo<StudentProfileDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
