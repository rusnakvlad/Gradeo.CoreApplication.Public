using Gradeo.CoreApplication.Application.Common.Attributes;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Application.Statistics.DTOs;
using Gradeo.CoreApplication.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gradeo.CoreApplication.Application.Statistics.Queries;

[Authorize(Permission.CanViewStatistics)]
public class GetStudentsCountPerStudyGroupQuery : IRequest<IEnumerable<ChartModel>>
{
    
}


public class GetStudentsCountPerStudyGroupQueryHandler : IRequestHandler<GetStudentsCountPerStudyGroupQuery,IEnumerable<ChartModel>>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IApplicationDbContext _applicationDbContext;
    
    public GetStudentsCountPerStudyGroupQueryHandler(ICurrentUserService currentUserService, IApplicationDbContext applicationDbContext)
    {
        _currentUserService = currentUserService;
        _applicationDbContext = applicationDbContext;
    }
    
    public async Task<IEnumerable<ChartModel>> Handle(GetStudentsCountPerStudyGroupQuery request, CancellationToken cancellationToken)
    {
        var groupWithStudents = await _applicationDbContext.StudyGroups.AsNoTracking()
            .Where(x => x.BusinessUnitId == _currentUserService.GetUserBusinessUnitId())
            .Include(x => x.StudentsInGroup)
            .Select(x => new ChartModel()
            {
                LabelId = x.Id,
                Label = x.Name,
                Value = x.StudentsInGroup.Count
            }).ToListAsync(cancellationToken);
        return groupWithStudents; 
    }
}

