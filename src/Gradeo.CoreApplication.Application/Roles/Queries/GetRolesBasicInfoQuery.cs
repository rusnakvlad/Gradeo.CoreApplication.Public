using AutoMapper;
using AutoMapper.QueryableExtensions;
using Gradeo.CoreApplication.Application.Common.Attributes;
using Gradeo.CoreApplication.Application.Common.Extensions;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Application.Roles.DTOs;
using Gradeo.CoreApplication.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gradeo.CoreApplication.Application.Roles.Queries;

[Authorize(Permission.CanViewRoles)]
public class GetRolesBasicInfoQuery : IRequest<IEnumerable<RoleBasicInfoDto>>
{
    public int? BusinessUnitId { get; set; }
}

public class GetRolesBasicInfoQueryHandler : IRequestHandler<GetRolesBasicInfoQuery, IEnumerable<RoleBasicInfoDto>>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;
    
    public GetRolesBasicInfoQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper, ICurrentUserService currentUserService)
    {
        _applicationDbContext = applicationDbContext;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<RoleBasicInfoDto>> Handle(GetRolesBasicInfoQuery request, CancellationToken cancellationToken)
    {
        var activeBuId = _currentUserService.GetUserBusinessUnitId();
        if (activeBuId.HasValue)
        {
            request.BusinessUnitId = activeBuId;
        }
        return await _applicationDbContext.Roles.AsNoTracking()
            .WhereIf(!request.BusinessUnitId.IsNullOrZero(), x => x.BusinessUnitId == request.BusinessUnitId || x.BusinessUnitType == BusinessUnitType.School)
            .WhereIf(request.BusinessUnitId.IsNullOrZero(), x => x.BusinessUnitId == null)
            .Select(x => new RoleBasicInfoDto()
            {
                Id = x.Id,
                RoleName = x.RoleName
            })
            .ToListAsync(cancellationToken);
    }
}
