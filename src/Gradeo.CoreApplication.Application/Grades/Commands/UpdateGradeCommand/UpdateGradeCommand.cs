using Gradeo.CoreApplication.Application.Common.Attributes;
using Gradeo.CoreApplication.Application.Common.Constants;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Domain.Entities;
using Gradeo.CoreApplication.Domain.Enums;
using MediatR;

namespace Gradeo.CoreApplication.Application.Grades.Commands.UpdateGradeCommand;

[Authorize(Permission.CanEditGrades)]
public class UpdateGradeCommand : IRequest
{
    public string Id { get; set; }
    public decimal Grade { get; set; }
}

public class UpdateGradeCommandHandler : IRequestHandler<UpdateGradeCommand>
{
    private readonly ICosmosDbContext _cosmosDbContext;
    private readonly ICurrentUserService _currentUserService;
    private readonly IApplicationDbContext _applicationDbContext;
    
    public UpdateGradeCommandHandler(ICosmosDbContext cosmosDbContext, ICurrentUserService currentUserService, IApplicationDbContext applicationDbContext)
    {
        _cosmosDbContext = cosmosDbContext;
        _currentUserService = currentUserService;
        _applicationDbContext = applicationDbContext;
    }
    
    public async Task<Unit> Handle(UpdateGradeCommand request, CancellationToken cancellationToken)
    {
        var gradesContainer = _cosmosDbContext.GetContainer(CosmosContainers.Grades);
        var query = $"SELECT * FROM c WHERE c.id = '{request.Id}'";
        var gradeIterator = gradesContainer.GetItemQueryIterator<GradeModel>(query);

        GradeModel grade = null;
        while (gradeIterator.HasMoreResults)
        {
            grade = (await gradeIterator.ReadNextAsync(cancellationToken)).FirstOrDefault();
        }

        
        grade.Grade = request.Grade;
        var result = await gradesContainer.ReplaceItemAsync(grade, grade.Id, cancellationToken: cancellationToken);
        return Unit.Value;
    }
}