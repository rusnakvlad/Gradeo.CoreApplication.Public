using Gradeo.CoreApplication.Application.Common.Extensions;
using Gradeo.CoreApplication.Application.Common.Helpers;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Domain.Entities;
using Gradeo.CoreApplication.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gradeo.CoreApplication.Application.Roles.Commands.UpsertRoleCommand;

public class UpsertRoleCommand : IRequest
{
    public int? Id { get; set; }
    public string RoleName { get; set; }
    public int? BusinessUnitId { get; set; }
    public List<Permission> Permissions { get; set; }
}

public class UpsertRoleCommandHandler : IRequestHandler<UpsertRoleCommand>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ICurrentUserService _currentUserService;
    
    public UpsertRoleCommandHandler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService)
    {
        _applicationDbContext = applicationDbContext;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(UpsertRoleCommand request, CancellationToken cancellationToken)
    {
        if (request.Permissions.IsNullOrEmpty())
        {
            throw new ArgumentException($"To create a role you need to select at least one permission");
        }

        if (request.BusinessUnitId is > 0)
        {
            if (!await _applicationDbContext.BusinessUnits.AsNoTracking().AnyAsync(x => x.Id == request.BusinessUnitId, cancellationToken))
            {
                throw new ArgumentException("Not found business unit");
            }
        }

        if (request.Id.IsNullOrZero())
        {
            PermissionHelper.ValidatePermissions(_currentUserService.GetCurrentUserPermissions(), Permission.CanCreateRoles);
            var userBUId = _currentUserService.GetUserBusinessUnitId();
            var role = new Role()
            {
                RoleName = request.RoleName,
                BusinessUnitType = userBUId.HasValue ? BusinessUnitType.School : BusinessUnitType.Gradeo,
                BusinessUnitId = userBUId ?? (request.BusinessUnitId is 0 ? null : request.BusinessUnitId),
                Permissions = request.Permissions.Select(x => new RolePermission()
                {
                    PermissionId = (int)x
                }).ToList()
            };

            await _applicationDbContext.Roles.AddAsync(role, cancellationToken);
        }
        else
        {
            PermissionHelper.ValidatePermissions(_currentUserService.GetCurrentUserPermissions(), Permission.CanEditRoles);
            var role = await _applicationDbContext.Roles
                .Include(x => x.Permissions)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (role == null)
            {
                throw new Exception($"Role with id ({request.Id}) was not found");
            }

            role.RoleName = request.RoleName;
            role.Permissions = request.Permissions.Select(x => new RolePermission()
            {
                PermissionId = (int)x
            }).ToList();
        }
        
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
