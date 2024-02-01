using Gradeo.CoreApplication.Application.MasterSubjects.Commands;
using Gradeo.CoreApplication.Application.MasterSubjects.DTOs;
using Gradeo.CoreApplication.Application.MasterSubjects.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gradeo.CoreApplication.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MasterSubjectsController : ApiControllerBase
{
    /// <summary>
    /// Upsert master subject  
    /// </summary>
    /// <param name="command"></param>
    [HttpPost]
    public async Task Upsert(UpsertMasterSubjectCommand command)
    {
        await Mediator.Send(command);
    }

    /// <summary>
    /// Get all master subject
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IEnumerable<MasterSubjectDto>> Get()
    {
        return await Mediator.Send(new GetMasterSubjectsQuery());
    }

}