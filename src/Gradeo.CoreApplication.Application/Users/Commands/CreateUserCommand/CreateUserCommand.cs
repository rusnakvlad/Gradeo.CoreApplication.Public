using AutoMapper;
using Gradeo.CoreApplication.Application.Common.Attributes;
using Gradeo.CoreApplication.Application.Common.Extensions;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Application.Common.Interfaces.Email;
using Gradeo.CoreApplication.Application.Users.DTOs;
using Gradeo.CoreApplication.Domain.Entities;
using Gradeo.CoreApplication.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gradeo.CoreApplication.Application.Users.Commands.CreateUserCommand;

[Authorize(Permission.CanCreateUsers)]
public class CreateUserCommand : IRequest<UserDto>
{
    public int? BusinessUnitId { get; set; }
    public UserDto UserMetadata { get; set; }
    public List<int>? RoleIds { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;
    private readonly IB2CManagementService _b2CManagementService;
    private readonly IEmailSenderService _emailSenderService;
    private readonly ICurrentUserService _currentUserService;
    
    public CreateUserCommandHandler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService,
        IMapper mapper, IB2CManagementService b2CManagementService, IEmailSenderService emailSenderService)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
        _currentUserService = currentUserService;
        _b2CManagementService = b2CManagementService;
        _emailSenderService = emailSenderService;
    }
    
    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var newUser = _mapper.Map<User>(request.UserMetadata);
        
        var currentUserBuId = _currentUserService.GetUserBusinessUnitId();

        if (currentUserBuId.HasValue)
        {
            newUser.BusinessUnitId = currentUserBuId;
        }
        else if (request.BusinessUnitId is > 0)
        {
            if (!await _applicationDbContext.BusinessUnits.AnyAsync(x => x.Id == request.BusinessUnitId,
                    cancellationToken))
            {
                throw new ArgumentException($"School with id (${request.BusinessUnitId}) does not exist");
            }

            newUser.BusinessUnitId = request.BusinessUnitId;
        }


        if (!request.RoleIds.IsNullOrEmpty())
        {
            newUser.UserRoles = request.RoleIds.Select(x => new UserRole()
            {
                RoleId = x
            }).ToList();
        }

        try
        {
            var b2CUserDetails = await _b2CManagementService.AddUserAsync(newUser, cancellationToken);
            if (b2CUserDetails is not null)
            {
                newUser.Id = new Guid(b2CUserDetails.Id);
                await _applicationDbContext.Users.AddAsync(newUser, cancellationToken);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);
                await _emailSenderService.SendWelcomeEmailAsync(b2CUserDetails, cancellationToken);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        var user = await _applicationDbContext.Users.AsNoTracking()
            .Where(u => u.Id == newUser.Id)
            .FirstOrDefaultAsync(cancellationToken);

        return _mapper.Map<UserDto>(user);
    }
}