using Gradeo.CoreApplication.Application.Menu.DTOs;
using Gradeo.CoreApplication.Application.Menu.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gradeo.CoreApplication.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MenuItemsController : ApiControllerBase
{
    /// <summary>
    /// Get menu for current user according to his permissions
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IEnumerable<MenuItemDto>> Get()
    {
        return await Mediator.Send(new GetMenuQuery());
    }

    /// <summary>
    /// Get menu items for admin data
    /// </summary>
    /// <returns></returns>
    [HttpGet("adminData")]
    public async Task<IEnumerable<MenuItemDto>> GetAdminDataMenuItems()
    {
        return await Mediator.Send(new GetAdminDataMenuQuery());
    }
}