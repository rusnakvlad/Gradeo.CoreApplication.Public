using Gradeo.CoreApplication.Application.Grades.Commands.CreateGradeCommand;
using Gradeo.CoreApplication.Application.Grades.Commands.DeleteGradeCommand;
using Gradeo.CoreApplication.Application.Grades.Commands.UpdateGradeCommand;
using Gradeo.CoreApplication.Application.Grades.DTOs;
using Gradeo.CoreApplication.Application.Grades.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gradeo.CoreApplication.WebAPI.Controllers;


[Authorize]
[ApiController]
[Route("api/[controller]")]
public class GradesController : ApiControllerBase
{
    /// <summary>
    /// Create student grade
    /// </summary>
    /// <param name="command"></param>
    [HttpPost]
    public async Task Create([FromBody] CreateGradeCommand command)
    {
        await Mediator.Send(command);
    }

    /// <summary>
    /// Update student grade
    /// </summary>
    /// <param name="command"></param>
    [HttpPut]
    public async Task Update([FromBody] UpdateGradeCommand command)
    {
        await Mediator.Send(command);
    }
    
    /// <summary>
    /// Get student grades by subject id
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpPost("currentStudentGrades")]
    public async Task<IEnumerable<GradeDto>> GetStudentGradesBySubjectId([FromBody] GetStudentGradesQuery query)
    {
        return await Mediator.Send(query);
    }

    /// <summary>
    /// Delete grade
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete("{id}")]
    public async Task Delete(string id)
    {
        await Mediator.Send(new DeleteGradeCommand() { Id = id });
    }

    /// <summary>
    /// Get students with their grades by study group and subject
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpPost("filtered")]
    public async Task<IEnumerable<StudentWithGradesDto>> GetStudentsWithGrades([FromBody] GetStudentsFilteredGradesQuery query)
    {
        return await Mediator.Send(query);
    }
}