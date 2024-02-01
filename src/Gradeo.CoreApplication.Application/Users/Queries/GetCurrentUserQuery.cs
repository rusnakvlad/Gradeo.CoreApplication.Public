using Gradeo.CoreApplication.Application.Common.Attributes;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Application.Users.DTOs;
using Gradeo.CoreApplication.Domain.Enums;
using MediatR;

namespace Gradeo.CoreApplication.Application.Users.Queries;

public class GetCurrentUserQuery : IRequest<UserDetailsDto>
{
    
}

public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, UserDetailsDto>
{
    private readonly ICurrentUserService _currentUserService;
    
    public GetCurrentUserQueryHandler(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }
    
    public async Task<UserDetailsDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        return _currentUserService.UserDetails;
    }
}