using AutoMapper;
using AutoMapper.QueryableExtensions;
using Gradeo.CoreApplication.Application.Common.Attributes;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Application.TeacherProfile.DTOs;
using Gradeo.CoreApplication.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gradeo.CoreApplication.Application.TeacherProfile.Queries;

[Authorize(Permission.CanViewTeachers)]
public class GetTeacherProfileByIdQuery : IRequest<TeacherProfileDto>
{
    public int? Id { get; set; }
}

public class GetTeacherProfileByIdQueryHandler : IRequestHandler<GetTeacherProfileByIdQuery, TeacherProfileDto>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;
    
    public GetTeacherProfileByIdQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper, ICurrentUserService currentUserService)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }
    
    public async Task<TeacherProfileDto> Handle(GetTeacherProfileByIdQuery request, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.TeacherProfiles.AsNoTracking()
            .Include(x => x.User)
            .Include(x => x.StudyGroups)
            .Include(x => x.AssignedSubjects)
            .ThenInclude(x => x.MasterSubject)
            .Where(x => x.UserId == _currentUserService.UserId)
            .ProjectTo<TeacherProfileDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }
}