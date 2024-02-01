using Gradeo.CoreApplication.Application.Common.Attributes;
using Gradeo.CoreApplication.Application.Common.Constants;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Application.Statistics.DTOs;
using Gradeo.CoreApplication.Domain.Entities;
using Gradeo.CoreApplication.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gradeo.CoreApplication.Application.Statistics.Queries;

[Authorize(Permission.CanViewStatistics)]
public class GetAverageGradePerGroupQuery : IRequest<IEnumerable<ChartModel>>
{
    
}


public class GetAverageGradePerGroupQueryHandler : IRequestHandler<GetAverageGradePerGroupQuery, IEnumerable<ChartModel>>
{
    private readonly ICosmosDbContext _cosmosDbContext;
    private readonly ICurrentUserService _currentUserService;
    private readonly IApplicationDbContext _applicationDbContext;
    
    public GetAverageGradePerGroupQueryHandler (ICosmosDbContext cosmosDbContext, ICurrentUserService currentUserService, IApplicationDbContext applicationDbContext)
    {
        _cosmosDbContext = cosmosDbContext;
        _currentUserService = currentUserService;
        _applicationDbContext = applicationDbContext;
    }
    public async Task<IEnumerable<ChartModel>> Handle(GetAverageGradePerGroupQuery request, CancellationToken cancellationToken)
    {
        var gradesContainer = _cosmosDbContext.GetContainer(CosmosContainers.Grades);
        var schoolId = _currentUserService.GetUserBusinessUnitId();
        
        var query = $"SELECT * FROM c WHERE c.schoolId = {schoolId}";
        var gradeIterator = gradesContainer.GetItemQueryIterator<GradeModel>(query);

        var grades = new List<ChartModel>();
        while (gradeIterator.HasMoreResults)
        {
            var gradesFromCosmos = await gradeIterator.ReadNextAsync(cancellationToken);
            grades.AddRange(gradesFromCosmos.Select(x => new ChartModel()
            {
                LabelId = x.StudyGroupId,
                Value = x.Grade,
            }));
        }

        var groupedGrades = grades.ToLookup(x => x.LabelId, x => x.Value);
        var averageGradePerGroup =
            groupedGrades.Select(x => new ChartModel() { LabelId = x.Key, Value = x.Average() }).ToList();
        await SetStudyGroupName(averageGradePerGroup, cancellationToken);
        return averageGradePerGroup;
    }
    
    private async Task SetStudyGroupName(List<ChartModel> grades, CancellationToken cancellationToken)
    {
        var studyGroupIds = grades.Select(x => x.LabelId).Distinct();
        var subjects = await _applicationDbContext.StudyGroups.Where(x => studyGroupIds.Contains(x.Id)).Select(x => new {x.Id, x.Name}).ToListAsync(cancellationToken);
        foreach (var grade in grades)
        {
            grade.Label = subjects.FirstOrDefault(x => x.Id == grade.LabelId)?.Name;
        }
    }
}
