using Gradeo.CoreApplication.Application.Common.DTOs;
using Gradeo.CoreApplication.Application.Common.Models;
using Gradeo.CoreApplication.Application.Schools.Commands;
using Gradeo.CoreApplication.Application.Schools.DTOs;
using Gradeo.CoreApplication.Application.Schools.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gradeo.CoreApplication.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SchoolsController : ApiControllerBase
{
    /// <summary>
    /// Create/Update school
    /// </summary>
    /// <param name="command"></param>
    [HttpPost]
    public async Task Create([FromBody] UpsertSchoolCommand command)
    {
        await Mediator.Send(command);
    }

    /// <summary>
    /// Get All Paged Schools
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<PaginatedList<SchoolInfoDto>> Get([FromQuery] GetSchoolsQuery query)
    {
        return await Mediator.Send(query);
    }

    /// <summary>
    /// Get All Schools
    /// </summary>
    /// <returns></returns>
    [HttpGet("all")]
    public async Task<IEnumerable<BusinessUnitDto>> GetAll()
    {
        return await Mediator.Send(new GetAllSchoolsQuery());
    }

    /// <summary>
    /// Delete list of schools by their ids
    /// </summary>
    /// <param name="command"></param>
    [HttpPost("delete")]
    public async Task DeleteByIds([FromBody] DeleteSchoolsCommand command)
    {
        await Mediator.Send(command);
    }

    /// <summary>
    /// Get school profile by id
    /// </summary>
    /// <returns></returns>
    [HttpGet("profile")]
    public async Task<SchoolProfileDto> GetById([FromQuery] GetSchoolProfileByIdQuery query)
    {
        return await Mediator.Send(query);
    }
}