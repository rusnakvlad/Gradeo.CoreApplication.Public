using Gradeo.CoreApplication.Application.Permissions.DTOs;
using Gradeo.CoreApplication.Application.Permissions.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gradeo.CoreApplication.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PermissionsController : ApiControllerBase
{
    /// <summary>
    /// Get all permissions
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IEnumerable<PermissionDto>> GetAll()
    {
        return await Mediator.Send(new GetPermissionsQuery());
    }
}