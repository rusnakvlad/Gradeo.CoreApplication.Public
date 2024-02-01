using Gradeo.CoreApplication.Application.Common.Models;
using Gradeo.CoreApplication.Application.TeacherProfile.DTOs;
using Gradeo.CoreApplication.Application.TeacherProfile.Queries;
using Gradeo.CoreApplication.Application.TeacherProfile.UpsertTeacherProfileCommand.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gradeo.CoreApplication.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TeacherProfilesController : ApiControllerBase
{
    /// <summary>
    /// Upsert teacher profile
    /// </summary>
    /// <param name="command"></param>
    [HttpPost]
    public async Task Create([FromBody] UpsertTeacherProfileCommand command)
    {
        await Mediator.Send(command);
    }

    /// <summary>
    /// Get teachers profiles
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<PaginatedList<TeacherProfileDto>> Get([FromQuery] GetTeacherProfilesQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpGet("profile")]
    public async Task<TeacherProfileDto> GetById([FromQuery] GetTeacherProfileByIdQuery query)
    {
        return await Mediator.Send(query);
    }
}