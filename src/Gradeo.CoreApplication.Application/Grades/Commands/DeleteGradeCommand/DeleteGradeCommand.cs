using Gradeo.CoreApplication.Application.Common.Attributes;
using Gradeo.CoreApplication.Application.Common.Constants;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Application.Grades.DTOs;
using MediatR;
using Microsoft.Azure.Cosmos;
using Permission = Gradeo.CoreApplication.Domain.Enums.Permission;

namespace Gradeo.CoreApplication.Application.Grades.Commands.DeleteGradeCommand;

[Authorize(Permission.CanDeleteGrades)]
public class DeleteGradeCommand : IRequest
{
    public string Id { get; set; }
}

public class DeleteGradeCommandHandler : IRequestHandler<DeleteGradeCommand>
{
    private readonly ICosmosDbContext _cosmosDbContext;
    
    public DeleteGradeCommandHandler(ICosmosDbContext cosmosDbContext)
    {
        _cosmosDbContext = cosmosDbContext;
    }
    
    public async Task<Unit> Handle(DeleteGradeCommand request, CancellationToken cancellationToken)
    {
        var gradesContainer = _cosmosDbContext.GetContainer(CosmosContainers.Grades);
        var result = await gradesContainer.DeleteItemAsync<object>(request.Id, new PartitionKey(request.Id), cancellationToken: cancellationToken);
        return Unit.Value;
    }
}

