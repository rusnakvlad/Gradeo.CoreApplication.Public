using AutoMapper;
using Gradeo.CoreApplication.Application.Common.Attributes;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Application.Users.DTOs;
using Gradeo.CoreApplication.Domain.Entities;
using Gradeo.CoreApplication.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gradeo.CoreApplication.Application.Users.Commands.UpdateUserCommand;

[Authorize(Permission.CanEditUsers)]
public class UpdateUserCommand : IRequest<UserDto>
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<int>? RoleIds { get; set; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IB2CManagementService _b2CManagementService;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;
    
    public UpdateUserCommandHandler(IApplicationDbContext applicationDbContext, IB2CManagementService b2CManagementService, IMapper mapper, ICacheService cacheService)
    {
        _applicationDbContext = applicationDbContext;
        _b2CManagementService = b2CManagementService;
        _mapper = mapper;
        _cacheService = cacheService;
    }
    
    public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
        if (user == null)
        {
            throw new Exception($"User with id({request.UserId}) was not found");
        }

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        
        var userRoles = request.RoleIds.Select(roleId => new UserRole
        {
            UserId = user.Id,
            RoleId = roleId
        });

        var savedUserRoles = _applicationDbContext.UserRoles.AsNoTracking().Where(x => x.UserId == request.UserId).ToList();

        var rolesToDelete = savedUserRoles.Where(x => userRoles.All(y => y.RoleId != x.RoleId));
        var rolesToAdd = userRoles.Where(x => savedUserRoles.All(y => y.RoleId != x.RoleId));

        if (rolesToDelete.Count() > savedUserRoles.Concat(rolesToAdd).Select(x => x.RoleId).Distinct().Count())
        {
            throw new Exception("You cannot delete all user's roles");
        }
        
        _applicationDbContext.UserRoles.RemoveRange(rolesToDelete.Select(x => new UserRole { RoleId = x.RoleId, UserId = request.UserId }));
        _applicationDbContext.UserRoles.AddRange(rolesToAdd.Select(x => new UserRole { RoleId = x.RoleId, UserId = request.UserId }));
        
        await _b2CManagementService.UpdateUserAsync(user, cancellationToken);

        await _applicationDbContext.SaveChangesAsync(cancellationToken);
        _cacheService.Remove(user.Id.ToString());
        
        return _mapper.Map<UserDto>(user);
    }
}