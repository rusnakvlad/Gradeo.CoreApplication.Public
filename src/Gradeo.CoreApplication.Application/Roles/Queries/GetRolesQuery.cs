using AutoMapper;
using AutoMapper.QueryableExtensions;
using Gradeo.CoreApplication.Application.Common.Attributes;
using Gradeo.CoreApplication.Application.Common.Constants;
using Gradeo.CoreApplication.Application.Common.Extensions;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Application.Common.Models;
using Gradeo.CoreApplication.Application.Common.QueryFilters;
using Gradeo.CoreApplication.Application.Roles.DTOs;
using Gradeo.CoreApplication.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gradeo.CoreApplication.Application.Roles.Queries;

[Authorize(Permission.CanViewRoles)]
public class GetRolesQuery : IRequest<PaginatedList<RoleDto>>, IPagination
{
    public int? SchoolId { get; set; }

    public int PageNumber { get; set; } = Pagination.DefaultPageNumber;
    public int PageSize { get; set; } = Pagination.DefaultPageSize;
}

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, PaginatedList<RoleDto>>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;
    
    public GetRolesQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper, ICurrentUserService currentUserService)
    {
        _applicationDbContext = applicationDbContext;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }
    
    public async Task<PaginatedList<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        var activeBuId = _currentUserService.GetUserBusinessUnitId();
        if (activeBuId.HasValue)
        {
            request.SchoolId = activeBuId;
        }
        
        return await _applicationDbContext.Roles.AsNoTracking()
            .WhereIf(!request.SchoolId.IsNullOrZero(), x => x.BusinessUnitId == request.SchoolId || x.BusinessUnitType == BusinessUnitType.School)
            .WhereIf(request.SchoolId.IsNullOrZero(), x => x.BusinessUnitId == null)
            .Include(x => x.Permissions)
            .ProjectTo<RoleDto>(_mapper.ConfigurationProvider)
            .PaginateListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}