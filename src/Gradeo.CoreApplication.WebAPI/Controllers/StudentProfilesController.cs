using Gradeo.CoreApplication.Application.Common.Models;
using Gradeo.CoreApplication.Application.StudentProfile.Commands.UpsertStudentProfileCommand;
using Gradeo.CoreApplication.Application.StudentProfile.DTOs;
using Gradeo.CoreApplication.Application.StudentProfile.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gradeo.CoreApplication.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class StudentProfilesController : ApiControllerBase
{
    /// <summary>
    /// Create student profile
    /// </summary>
    /// <param name="command"></param>
    [HttpPost]
    public async Task Create([FromBody] UpsertStudentProfileCommand command)
    {
        await Mediator.Send(command);
    }

    /// <summary>
    /// Get students profiles
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<PaginatedList<StudentProfileDto>> Get([FromQuery] GetStudentProfilesQuery query)
    {
        return await Mediator.Send(query);
    }

    /// <summary>
    /// Get student profile
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet("profile")]
    public async Task<StudentProfileDto> GetById([FromQuery] GetStudentProfileByIdQuery query)
    {
        return await Mediator.Send(query);
    }
}