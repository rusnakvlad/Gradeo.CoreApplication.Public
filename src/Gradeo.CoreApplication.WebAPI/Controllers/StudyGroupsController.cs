using Gradeo.CoreApplication.Application.Common.Models;
using Gradeo.CoreApplication.Application.MasterSubjects.DTOs;
using Gradeo.CoreApplication.Application.StudyGroups.Commands.UpsertStudyGroupCommand;
using Gradeo.CoreApplication.Application.StudyGroups.DTOs;
using Gradeo.CoreApplication.Application.StudyGroups.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gradeo.CoreApplication.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class StudyGroupsController : ApiControllerBase
{
    /// <summary>
    /// Upsert study group
    /// </summary>
    /// <param name="command"></param>
    [HttpPost]
    public async Task Upsert(UpsertStudyGroupCommand command)
    {
        await Mediator.Send(command);
    }

    /// <summary>
    /// Get paged study groups
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<PaginatedList<StudyGroupDto>> Get([FromQuery] GetStudyGroupsQuery query)
    {
        return await Mediator.Send(query);
    }

    /// <summary>
    /// Get study group options
    /// </summary>
    /// <returns></returns>
    [HttpGet("options")]
    public async Task<IEnumerable<StudyGroupBasicInfoDto>> GetAll()
    {
        return await Mediator.Send(new GetAllActiveStudyGroupsQuery());
    }

    [HttpGet("{id}")]
    public async Task<IEnumerable<MasterSubjectDto>> GetGroupSubjects(int id)
    {
        return await Mediator.Send(new GetStudyGroupSubjectsQuery() { GroupId = id });
    }
}