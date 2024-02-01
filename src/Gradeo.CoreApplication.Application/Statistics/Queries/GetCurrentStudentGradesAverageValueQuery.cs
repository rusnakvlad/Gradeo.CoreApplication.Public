using Gradeo.CoreApplication.Application.Common.Attributes;
using Gradeo.CoreApplication.Application.Common.Constants;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Application.Statistics.DTOs;
using Gradeo.CoreApplication.Domain.Entities;
using Gradeo.CoreApplication.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gradeo.CoreApplication.Application.Statistics.Queries;

public class GetCurrentStudentGradesAverageValueQuery : IRequest<IEnumerable<ChartModel>>
{
    
}

public class GetCurrentStudentGradesAverageValueQueryHandler : IRequestHandler<GetCurrentStudentGradesAverageValueQuery, IEnumerable<ChartModel>>
{
    private readonly ICosmosDbContext _cosmosDbContext;
    private readonly ICurrentUserService _currentUserService;
    private readonly IApplicationDbContext _applicationDbContext;
    
    public GetCurrentStudentGradesAverageValueQueryHandler (ICosmosDbContext cosmosDbContext, ICurrentUserService currentUserService, IApplicationDbContext applicationDbContext)
    {
        _cosmosDbContext = cosmosDbContext;
        _currentUserService = currentUserService;
        _applicationDbContext = applicationDbContext;
    }
    
    public async Task<IEnumerable<ChartModel>> Handle(GetCurrentStudentGradesAverageValueQuery request, CancellationToken cancellationToken)
    {
        var gradesContainer = _cosmosDbContext.GetContainer(CosmosContainers.Grades);
        var studentId =
            (await _applicationDbContext.StudentProfiles.FirstOrDefaultAsync(x => x.UserId == _currentUserService.UserId,
                cancellationToken))?.Id;
        var query = $"SELECT * FROM c WHERE c.studentId = {studentId}";
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
        var averageCalculatedGrades =
            groupedGrades.Select(x => new ChartModel() { LabelId = x.Key, Value = x.Average() }).ToList();
        await SetSubjectName(averageCalculatedGrades, cancellationToken);
        return averageCalculatedGrades;
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