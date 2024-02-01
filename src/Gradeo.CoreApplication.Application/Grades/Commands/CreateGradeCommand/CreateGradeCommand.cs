using Gradeo.CoreApplication.Application.Common.Attributes;
using Gradeo.CoreApplication.Application.Common.Constants;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Domain.Entities;
using Gradeo.CoreApplication.Domain.Enums;
using MediatR;

namespace Gradeo.CoreApplication.Application.Grades.Commands.CreateGradeCommand;

[Authorize(Permission.CanCreateGrades)]
public class CreateGradeCommand : IRequest
{
    public decimal Grade { get; set; }
    public int StudyGroupId { get; set; }
    public int StudentId { get; set; }
    public int SubjectId { get; set; }
    public DateTimeOffset Date { get; set; }
}

public class CreateGradeCommandHandler : IRequestHandler<CreateGradeCommand>
{
    private readonly ICosmosDbContext _cosmosDbContext;
    private readonly ICurrentUserService _currentUserService;
    private readonly IApplicationDbContext _applicationDbContext;
    
    public CreateGradeCommandHandler(ICosmosDbContext cosmosDbContext, ICurrentUserService currentUserService, IApplicationDbContext applicationDbContext)
    {
        _cosmosDbContext = cosmosDbContext;
        _currentUserService = currentUserService;
        _applicationDbContext = applicationDbContext;
    }
    
    public async Task<Unit> Handle(CreateGradeCommand request, CancellationToken cancellationToken)
    {
        var gradesContainer = _cosmosDbContext.GetContainer(CosmosContainers.Grades);

        var grade = new GradeModel()
        {
            Id = Guid.NewGuid().ToString(),
            Grade = request.Grade,
            StudentId = request.StudentId,
            StudyGroupId = request.StudyGroupId,
            SubjectId = request.SubjectId,
            Date = request.Date,
            SchoolId = _currentUserService.GetUserBusinessUnitId().Value,
            CreatedBy = _currentUserService.UserId?.ToString(),
            CreatedDate = DateTimeOffset.Now,
        };
            
        var result = await gradesContainer.CreateItemAsync(grade, cancellationToken: cancellationToken);
        
        return Unit.Value;
    }
}
