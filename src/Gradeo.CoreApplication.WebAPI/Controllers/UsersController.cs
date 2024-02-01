using Gradeo.CoreApplication.Application.Common.Models;
using Gradeo.CoreApplication.Application.Users.Commands.CreateUserCommand;
using Gradeo.CoreApplication.Application.Users.Commands.UpdateUserCommand;
using Gradeo.CoreApplication.Application.Users.DTOs;
using Gradeo.CoreApplication.Application.Users.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Gradeo.CoreApplication.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ApiControllerBase
{
    /// <summary>
    /// Get paged users per school if selected
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<PaginatedList<UserDto>> GetUsers([FromQuery] GetUsersQuery query)
    {
        return await Mediator.Send(query);
    }
    
    /// <summary>
    /// Create user in DB and B2C
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<UserDto> Create([FromBody] CreateUserCommand command)
    {
        return await Mediator.Send(command);
    }

    /// <summary>
    /// Update user in DB and B2C
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<UserDto> Update([FromBody] UpdateUserCommand command)
    {
        return await Mediator.Send(command);
    }
    
    /// <summary>
    /// Get current user from token
    /// </summary>
    /// <returns></returns>
    [HttpGet("currentUser")]
    public async Task<UserDetailsDto> GetCurrentUser()
    {
        return await Mediator.Send(new GetCurrentUserQuery());
    }

    /// <summary>
    /// Get users email by current business unit
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet("filteredEmails")]
    public async Task<IEnumerable<string>> GetEmails([FromQuery] GetFilteredUsersEmailsQuery query)
    {
        return await Mediator.Send(query);
    }
}