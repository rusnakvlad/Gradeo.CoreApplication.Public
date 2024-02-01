using Gradeo.CoreApplication.Application.Common.Attributes;
using Gradeo.CoreApplication.Application.Common.DTOs;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gradeo.CoreApplication.Application.Schools.Queries;

[Authorize(Permission.CanViewSchools)]
public class GetAllSchoolsQuery : IRequest<IEnumerable<BusinessUnitDto>>
{
    
}

public class GetAllSchoolsQueryHandler : IRequestHandler<GetAllSchoolsQuery, IEnumerable<BusinessUnitDto>>
{
    private readonly IApplicationDbContext _applicationDbContext;
    
    public GetAllSchoolsQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    
    public async Task<IEnumerable<BusinessUnitDto>> Handle(GetAllSchoolsQuery request, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.BusinessUnits.AsNoTracking()
            .Where(x => x.BusinessUnitType == BusinessUnitType.School)
            .Select(x => new BusinessUnitDto()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync(cancellationToken);
    }
}