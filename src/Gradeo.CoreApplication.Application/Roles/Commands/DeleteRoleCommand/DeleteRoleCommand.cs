using Gradeo.CoreApplication.Application.Common.Attributes;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Domain.Entities;
using Gradeo.CoreApplication.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gradeo.CoreApplication.Application.Roles.Commands.DeleteRoleCommand;

[Authorize(Permission.CanDeleteRoles)]
public class DeleteRoleCommand : IRequest
{
    public int Id { get; set; }
}

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
{
    private readonly IApplicationDbContext _applicationDbContext;
    
    public DeleteRoleCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    
    public async Task<Unit> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var isRoleInUsing = (await _applicationDbContext.UserRoles.Where(x => x.RoleId == request.Id)
            .CountAsync(cancellationToken)) > 0;

        if (isRoleInUsing)
        {
            throw new Exception("Role is in using");
        }

        _applicationDbContext.Roles.Remove(new Role() { Id = request.Id });
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}