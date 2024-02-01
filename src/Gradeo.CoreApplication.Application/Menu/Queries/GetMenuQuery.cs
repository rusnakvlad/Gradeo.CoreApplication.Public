using Gradeo.CoreApplication.Application.Common.Constants;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Application.Menu.DTOs;
using Gradeo.CoreApplication.Domain.Enums;
using MediatR;

namespace Gradeo.CoreApplication.Application.Menu.Queries;

public class GetMenuQuery : IRequest<IEnumerable<MenuItemDto>>
{
    
}

public class GetMenuQueryHandler : IRequestHandler<GetMenuQuery, IEnumerable<MenuItemDto>>
{
    private readonly ICurrentUserService _currentUserService;
    
    public GetMenuQueryHandler(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }
    
    public async Task<IEnumerable<MenuItemDto>> Handle(GetMenuQuery request, CancellationToken cancellationToken)
    {
        var userDetails = _currentUserService.UserDetails;

        var menuItems = userDetails.UserType switch
        {
            UserType.Student => MenuConstants.MenuItems.Where(x => x.StudentView),
            UserType.Teacher => MenuConstants.MenuItems.Where(x => x.TeacherView),
            // school admin
            null when userDetails.BusinessUnitId.HasValue => MenuConstants.MenuItems.Where(x => x.SchoolAdminView),
            // gradeo admin
            _ => MenuConstants.MenuItems.Where(x => x.GradeoAdminView)
        };

        var accessibleMenuItems = menuItems.Where(x => x.RequiredPermission == null || userDetails.Permissions.Contains((int)x.RequiredPermission));

        return accessibleMenuItems;
    }
}