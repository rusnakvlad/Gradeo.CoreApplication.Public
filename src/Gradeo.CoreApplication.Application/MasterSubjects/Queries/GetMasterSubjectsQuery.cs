using Gradeo.CoreApplication.Application.Common.Attributes;
using Gradeo.CoreApplication.Application.Common.Extensions;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Application.MasterSubjects.DTOs;
using Gradeo.CoreApplication.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gradeo.CoreApplication.Application.MasterSubjects.Queries;

[Authorize(Permission.CanViewMasterSubjects)]
public class GetMasterSubjectsQuery : IRequest<IEnumerable<MasterSubjectDto>>
{
    
}

public class GetMasterSubjectsQueryHandler : IRequestHandler<GetMasterSubjectsQuery, IEnumerable<MasterSubjectDto>>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ICurrentUserService _currentUserService;
    
    public GetMasterSubjectsQueryHandler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService)
    {
        _applicationDbContext = applicationDbContext;
        _currentUserService = currentUserService;
    }
    
    public async Task<IEnumerable<MasterSubjectDto>> Handle(GetMasterSubjectsQuery request, CancellationToken cancellationToken)
    {
        var userBusinessUnitId = _currentUserService.GetUserBusinessUnitId();
        
        return await _applicationDbContext.MasterSubjects.AsNoTracking()
            .WhereIf(!userBusinessUnitId.IsNullOrZero(), x => x.BusinessUnitId == userBusinessUnitId)
            .WhereIf(userBusinessUnitId.IsNullOrZero(), x => x.BusinessUnitId == null)
            .Select(x => new MasterSubjectDto()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync(cancellationToken);
    }
}