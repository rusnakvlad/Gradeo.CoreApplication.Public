using AutoMapper;
using Gradeo.CoreApplication.Application.Common.Attributes;
using Gradeo.CoreApplication.Application.Common.Constants;
using Gradeo.CoreApplication.Application.Common.Extensions;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Application.Common.Models;
using Gradeo.CoreApplication.Application.Common.QueryFilters;
using Gradeo.CoreApplication.Application.Users.DTOs;
using Gradeo.CoreApplication.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gradeo.CoreApplication.Application.Users.Queries;

[Authorize(Permission.CanViewUsers)]
public class GetUsersQuery : IRequest<PaginatedList<UserDto>>, IPagination
{
    public int? SchoolId { get; set; }
    public int PageNumber { get; set; } = Pagination.DefaultPageNumber;
    public int PageSize { get; set; } = Pagination.DefaultPageSize;
}

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PaginatedList<UserDto>>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;
    
    public GetUsersQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper, ICurrentUserService currentUserService)
    {
        _applicationDbContext = applicationDbContext;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }
    
    public async Task<PaginatedList<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var activeBuId = _currentUserService.GetUserBusinessUnitId();
        if (activeBuId.HasValue)
        {
            request.SchoolId = activeBuId;
        }
        return await _applicationDbContext.Users.AsNoTracking()
            .Include(x => x.UserRoles)
            .WhereIf(!request.SchoolId.IsNullOrZero(), x => x.BusinessUnitId == request.SchoolId)
            .WhereIf(request.SchoolId.IsNullOrZero(), x => x.BusinessUnitId == null)
            .Select(x => _mapper.Map<UserDto>(x))
            .PaginateListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}