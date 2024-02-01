using Gradeo.CoreApplication.Application.Permissions.DTOs;
using Gradeo.CoreApplication.Domain.Enums;
using MediatR;

namespace Gradeo.CoreApplication.Application.Permissions.Queries;

public class GetPermissionsQuery : IRequest<IEnumerable<PermissionDto>>
{
    
}

public class GetPermissionsQueryHandler : IRequestHandler<GetPermissionsQuery, IEnumerable<PermissionDto>>
{
    public async Task<IEnumerable<PermissionDto>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
    {
        return Enum.GetValues<Permission>().Select(x => new PermissionDto() { PermissionId = x });
    }
}