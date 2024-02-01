using Gradeo.CoreApplication.Application.Common.Models;
using Gradeo.CoreApplication.Application.Roles.Commands.UpsertRoleCommand;
using Gradeo.CoreApplication.Application.Roles.Commands.DeleteRoleCommand;
using Gradeo.CoreApplication.Application.Roles.DTOs;
using Gradeo.CoreApplication.Application.Roles.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gradeo.CoreApplication.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class RolesController : ApiControllerBase
{
    /// <summary>
    /// Create role
    /// </summary>
    /// <param name="command"></param>
    [HttpPost]
    public async Task Create([FromBody] UpsertRoleCommand command)
    {
        await Mediator.Send(command);
    }

    /// <summary>
    /// Delete role
    /// </summary>
    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await Mediator.Send(new DeleteRoleCommand() { Id = id });
    }

    /// <summary>
    /// Get paginated roles with support to filter by business unit
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<PaginatedList<RoleDto>> Get([FromQuery] GetRolesQuery query)
    {
        return await Mediator.Send(query);
    }

    /// <summary>
    /// Get all roles per business unit
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet("all")]
    public async Task<IEnumerable<RoleBasicInfoDto>> GetAll([FromQuery] GetRolesBasicInfoQuery query)
    {
        return await Mediator.Send(query);
    }
}