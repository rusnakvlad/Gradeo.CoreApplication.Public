using Gradeo.CoreApplication.Application.Common.Attributes;
using Gradeo.CoreApplication.Application.Common.Constants;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Application.Grades.DTOs;
using Gradeo.CoreApplication.Domain.Entities;
using Gradeo.CoreApplication.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gradeo.CoreApplication.Application.Grades.Queries;

[Authorize(Permission.CanViewGrades)]
public class GetStudentGradesQuery : IRequest<IEnumerable<GradeDto>>
{
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
}

public class GetStudentGradesQueryHandler : IRequestHandler<GetStudentGradesQuery, IEnumerable<GradeDto>>
{
    private readonly ICosmosDbContext _cosmosDbContext;
    private readonly ICurrentUserService _currentUserService;
    private readonly IApplicationDbContext _applicationDbContext;
    
    public GetStudentGradesQueryHandler (ICosmosDbContext cosmosDbContext, ICurrentUserService currentUserService, IApplicationDbContext applicationDbContext)
    {
        _cosmosDbContext = cosmosDbContext;
        _currentUserService = currentUserService;
        _applicationDbContext = applicationDbContext;
    }
    public async Task<IEnumerable<GradeDto>> Handle(GetStudentGradesQuery request, CancellationToken cancellationToken)
    {
        var gradesContainer = _cosmosDbContext.GetContainer(CosmosContainers.Grades);
        var studentId =
            (await _applicationDbContext.StudentProfiles.FirstOrDefaultAsync(x => x.UserId == _currentUserService.UserId,
                cancellationToken))?.Id;
        
        var dateFilter = $"AND c.date >= '{request.StartDate.Date.ToString("yyyy-MM-dd")}' AND c.date <= '{request.EndDate.ToString("yyyy-MM-dd")}'";
        var query = $"SELECT * FROM c WHERE c.studentId = {studentId} " + dateFilter;
        var gradeIterator = gradesContainer.GetItemQueryIterator<GradeModel>(query);

        var grades = new List<GradeDto>();
        while (gradeIterator.HasMoreResults)
        {
            var gradesFromCosmos = await gradeIterator.ReadNextAsync(cancellationToken);
            grades.AddRange(gradesFromCosmos.Select(x => new GradeDto()
            {
                Id = x.Id,
                Grade = x.Grade,
                SubjectId = x.SubjectId,
                StudentId = x.StudentId,
                Date = x.Date
            }));
        }

        await SetSubjectName(grades, cancellationToken);
        
        return grades;

    }

    private async Task SetSubjectName(List<GradeDto> grades, CancellationToken cancellationToken)
    {
        var subjectIds = grades.Select(x => x.SubjectId).Distinct();
        var subjects = await _applicationDbContext.MasterSubjects.Where(x => subjectIds.Contains(x.Id)).Select(x => new {x.Id, x.Name}).ToListAsync(cancellationToken);
        foreach (var grade in grades)
        {
            grade.SubjectName = subjects.FirstOrDefault(x => x.Id == grade.SubjectId)?.Name;
        }
    }
}