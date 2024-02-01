using Gradeo.CoreApplication.Application.Common.Constants;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Application.Menu.DTOs;
using Gradeo.CoreApplication.Domain.Enums;
using MediatR;

namespace Gradeo.CoreApplication.Application.Menu.Queries;

public class GetAdminDataMenuQuery : IRequest<IEnumerable<MenuItemDto>>
{
    
}

public class GetAdminDataMenuQueryHandler : IRequestHandler<GetAdminDataMenuQuery, IEnumerable<MenuItemDto>>
{
    public GetAdminDataMenuQueryHandler(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    private readonly ICurrentUserService _currentUserService;

    public async Task<IEnumerable<MenuItemDto>> Handle(GetAdminDataMenuQuery request, CancellationToken cancellationToken)
    {
        var userDetails = _currentUserService.UserDetails;

        var menuItems = userDetails.UserType switch
        {
            UserType.Student => MenuConstants.MenuAdminDataItems.Where(x => x.StudentView),
            UserType.Teacher => MenuConstants.MenuAdminDataItems.Where(x => x.TeacherView),
            // school admin
            null when userDetails.BusinessUnitId.HasValue => MenuConstants.MenuAdminDataItems.Where(x => x.SchoolAdminView),
            // gradeo admin
            _ => MenuConstants.MenuAdminDataItems.Where(x => x.GradeoAdminView)
        };

        var accessibleMenuItems = menuItems.Where(x => x.RequiredPermission == null || userDetails.Permissions.Contains((int)x.RequiredPermission));

        return accessibleMenuItems;
    }
}