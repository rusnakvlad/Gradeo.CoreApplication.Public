using Gradeo.CoreApplication.Application.Common.Helpers;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Domain.Entities;
using Gradeo.CoreApplication.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gradeo.CoreApplication.Application.MasterSubjects.Commands;

public class UpsertMasterSubjectCommand : IRequest
{
    public int? Id { get; set; }
    public string Name { get; set; }
}

public class UpsertMasterSubjectCommandHandler : IRequestHandler<UpsertMasterSubjectCommand>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ICurrentUserService _currentUserService;
    
    public UpsertMasterSubjectCommandHandler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService)
    {
        _applicationDbContext = applicationDbContext;
        _currentUserService = currentUserService;
    }


    public async Task<Unit> Handle(UpsertMasterSubjectCommand request, CancellationToken cancellationToken)
    {
        var userBusinessUnitId = _currentUserService.GetUserBusinessUnitId();
        var userPermissions = _currentUserService.GetCurrentUserPermissions();
        
        if (request.Id is > 0)
        {
            PermissionHelper.ValidatePermissions(userPermissions, Permission.CanEditMasterSubjects);
            var masterSubject =
                await _applicationDbContext.MasterSubjects.FirstOrDefaultAsync(x => x.Id == request.Id,
                    cancellationToken);
            masterSubject.Name = request.Name;
        }
        else
        {
            PermissionHelper.ValidatePermissions(userPermissions, Permission.CanCreateMasterSubjects);
            _applicationDbContext.MasterSubjects.Add(new MasterSubject()
            {
                Name = request.Name,
                BusinessUnitId = userBusinessUnitId
            });
        }

        await _applicationDbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}