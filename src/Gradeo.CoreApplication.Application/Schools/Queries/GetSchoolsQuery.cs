using Gradeo.CoreApplication.Application.Common.Attributes;
using Gradeo.CoreApplication.Application.Common.Constants;
using Gradeo.CoreApplication.Application.Common.Extensions;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Application.Common.Models;
using Gradeo.CoreApplication.Application.Common.QueryFilters;
using Gradeo.CoreApplication.Application.Schools.DTOs;
using Gradeo.CoreApplication.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gradeo.CoreApplication.Application.Schools.Queries;

[Authorize(Permission.CanViewSchools)]
public class GetSchoolsQuery : IRequest<PaginatedList<SchoolInfoDto>>, IPagination
{
    public int PageNumber { get; set; } = Pagination.DefaultPageNumber;
    public int PageSize { get; set; } = Pagination.DefaultPageSize;
}

public class GetSchoolsQueryHandler : IRequestHandler<GetSchoolsQuery, PaginatedList<SchoolInfoDto>>
{
    private readonly IApplicationDbContext _applicationDbContext;
    
    public GetSchoolsQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<PaginatedList<SchoolInfoDto>> Handle(GetSchoolsQuery request, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.SchoolsInfo.AsNoTracking()
            .Include(x => x.BusinessUnit)
            .Select(x => new SchoolInfoDto()
            {
                Id = x.Id,
                Name = x.BusinessUnit.Name,
                Country = x.Country,
                City = x.City
            }).PaginateListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}