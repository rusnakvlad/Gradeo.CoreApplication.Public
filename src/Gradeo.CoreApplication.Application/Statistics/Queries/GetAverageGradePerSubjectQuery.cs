using Gradeo.CoreApplication.Application.Common.Constants;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Application.Statistics.DTOs;
using Gradeo.CoreApplication.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gradeo.CoreApplication.Application.Statistics.Queries;

public class GetAverageGradePerSubjectQuery : IRequest<IEnumerable<ChartModel>>
{
    
}

public class GetAverageGradePerSubjectQueryHandler : IRequestHandler<GetAverageGradePerSubjectQuery, IEnumerable<ChartModel>>
{
    private readonly ICosmosDbContext _cosmosDbContext;
    private readonly ICurrentUserService _currentUserService;
    private readonly IApplicationDbContext _applicationDbContext;
    
    public GetAverageGradePerSubjectQueryHandler (ICosmosDbContext cosmosDbContext, ICurrentUserService currentUserService, IApplicationDbContext applicationDbContext)
    {
        _cosmosDbContext = cosmosDbContext;
        _currentUserService = currentUserService;
        _applicationDbContext = applicationDbContext;
    }
    
    public async Task<IEnumerable<ChartModel>> Handle(GetAverageGradePerSubjectQuery request, CancellationToken cancellationToken)
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
                LabelId = x.SubjectId,
                Value = x.Grade,
            }));
        }

        var groupedGrades = grades.ToLookup(x => x.LabelId, x => x.Value);
        var averageGradePerSubject =
            groupedGrades.Select(x => new ChartModel() { LabelId = x.Key, Value = x.Average() }).ToList();
        await SetSubjectName(averageGradePerSubject, cancellationToken);
        return averageGradePerSubject;
    }
    
    private async Task SetSubjectName(List<ChartModel> grades, CancellationToken cancellationToken)
    {
        var subjectIds = grades.Select(x => x.LabelId).Distinct();
        var subjects = await _applicationDbContext.MasterSubjects.Where(x => subjectIds.Contains(x.Id)).Select(x => new {x.Id, x.Name}).ToListAsync(cancellationToken);
        foreach (var grade in grades)
        {
            grade.Label = subjects.FirstOrDefault(x => x.Id == grade.LabelId)?.Name;
        }
    }
}