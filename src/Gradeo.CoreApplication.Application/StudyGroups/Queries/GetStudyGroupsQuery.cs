using AutoMapper;
using AutoMapper.QueryableExtensions;
using Gradeo.CoreApplication.Application.Common.Attributes;
using Gradeo.CoreApplication.Application.Common.Constants;
using Gradeo.CoreApplication.Application.Common.Extensions;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Application.Common.Models;
using Gradeo.CoreApplication.Application.Common.QueryFilters;
using Gradeo.CoreApplication.Application.StudyGroups.DTOs;
using Gradeo.CoreApplication.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gradeo.CoreApplication.Application.StudyGroups.Queries;

[Authorize(Permission.CanViewStudyGroups)]
public class GetStudyGroupsQuery : IRequest<PaginatedList<StudyGroupDto>>, IPagination
{
    public int PageNumber { get; set; } = Pagination.DefaultPageNumber;
    public int PageSize { get; set; } = Pagination.DefaultPageSize;
}

public class GetStudyGroupsQueryHandler : IRequestHandler<GetStudyGroupsQuery, PaginatedList<StudyGroupDto>>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    
    public GetStudyGroupsQueryHandler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService, IMapper mapper)
    {
        _applicationDbContext = applicationDbContext;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }
    
    public async Task<PaginatedList<StudyGroupDto>> Handle(GetStudyGroupsQuery request, CancellationToken cancellationToken)
    {
        var currentUserSchoolId = _currentUserService.GetUserBusinessUnitId();
        return await _applicationDbContext.StudyGroups.AsNoTracking()
            .Include(x => x.StudyGroupSubjects)
            .Where(x => x.BusinessUnitId == currentUserSchoolId)
            .ProjectTo<StudyGroupDto>(_mapper.ConfigurationProvider)
            .PaginateListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}