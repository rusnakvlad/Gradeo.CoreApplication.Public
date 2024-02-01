using Gradeo.CoreApplication.Application.Statistics.DTOs;
using Gradeo.CoreApplication.Application.Statistics.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.CallRecords;

namespace Gradeo.CoreApplication.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class StatisticsController : ApiControllerBase
{
    /// <summary>
    /// Get current student average grade per subject
    /// </summary>
    /// <returns></returns>
    [HttpGet("studentAverageGradePerSubject")]
    public async Task<IEnumerable<ChartModel>> GetCurrentStudentAverageSubjectGrade()
    {
        return await Mediator.Send(new GetCurrentStudentGradesAverageValueQuery());
    }

    /// <summary>
    /// Get students count per study group in school
    /// </summary>
    /// <returns></returns>
    [HttpGet("studentsPerGroup")]
    public async Task<IEnumerable<ChartModel>> GetStudentsCountPerStudyGroup()
    {
        return await Mediator.Send(new GetStudentsCountPerStudyGroupQuery());
    }

    /// <summary>
    /// Get average grade per study group in school
    /// </summary>
    /// <returns></returns>
    [HttpGet("averageGradePerGroup")]
    public async Task<IEnumerable<ChartModel>> GetAverageGradePerGroup()
    {
        return await Mediator.Send(new GetAverageGradePerGroupQuery());
    }

    [HttpGet("averageGradePerSubject")]
    public async Task<IEnumerable<ChartModel>> GetAverageGradePerSubject()
    {
        return await Mediator.Send(new GetAverageGradePerSubjectQuery());
    }
}