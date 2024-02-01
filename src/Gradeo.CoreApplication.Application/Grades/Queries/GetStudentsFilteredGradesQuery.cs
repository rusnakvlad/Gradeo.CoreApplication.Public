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
public class GetStudentsFilteredGradesQuery : IRequest<IEnumerable<StudentWithGradesDto>>
{
    public int StudyGroupId { get; set; }
    public int SubjectId { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
}

public class GetStudentsFilteredGradesQueryHandler : IRequestHandler<GetStudentsFilteredGradesQuery, IEnumerable<StudentWithGradesDto>>
{
    private readonly ICosmosDbContext _cosmosDbContext;
    private readonly ICurrentUserService _currentUserService;
    private readonly IApplicationDbContext _applicationDbContext;
    
    public GetStudentsFilteredGradesQueryHandler (ICosmosDbContext cosmosDbContext, ICurrentUserService currentUserService, IApplicationDbContext applicationDbContext)
    {
        _cosmosDbContext = cosmosDbContext;
        _currentUserService = currentUserService;
        _applicationDbContext = applicationDbContext;
    }
    public async Task<IEnumerable<StudentWithGradesDto>> Handle(GetStudentsFilteredGradesQuery request, CancellationToken cancellationToken)
    {
        var studentsWithGrades = await _applicationDbContext.StudentProfiles.AsNoTracking()
            .Include(x => x.User)
            .Include(x => x.StudyGroups)
            .Where(x => x.StudyGroups.Any(sg => sg.StudyGroupId == request.StudyGroupId))
            .Select(x => new StudentWithGradesDto()
            {
                StudentId = x.Id,
                StudentFirstName = x.User.FirstName,
                StudentLastName = x.User.LastName
            }).ToListAsync(cancellationToken);
        
        var gradesContainer = _cosmosDbContext.GetContainer(CosmosContainers.Grades);
        var dateFilter = $"c.date >= '{request.StartDate.Date.ToString("yyyy-MM-dd")}' AND c.date <= '{request.EndDate.ToString("yyyy-MM-dd")}'";
        var query = $"SELECT * FROM c WHERE c.subjectId = {request.SubjectId} AND c.studyGroupId = {request.StudyGroupId} AND " + dateFilter;
        
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
        SetStudentGrades(studentsWithGrades, grades);

        return studentsWithGrades;

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

    private void SetStudentGrades(List<StudentWithGradesDto> studentWithGrades, List<GradeDto> grades)
    {
        foreach (var student in studentWithGrades)
        {
            student.Grades = grades.Where(x => x.StudentId == student.StudentId);
        }
    }
}