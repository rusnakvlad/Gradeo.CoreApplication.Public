using System.Reflection;
using Gradeo.CoreApplication.Application.Common.Attributes;
using Gradeo.CoreApplication.Application.Common.Exceptions;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using MediatR;

namespace Gradeo.CoreApplication.Application.Common.Behaviours;

public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
{
    private readonly ICurrentUserService _currentUserService;
    
    public AuthorizationBehaviour(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();
        
        if (authorizeAttributes.Any())
        {
            // Must be authenticated user
            var userDetails = _currentUserService.UserDetails;
            _ = userDetails ?? throw new UnauthorizedAccessException();

            var requiredPermissions = authorizeAttributes.Where(a => a.Permissions != null).SelectMany(a => a.Permissions).Select(x => (int)x).ToArray();
            var userPermissions = userDetails.Permissions;
            var requestName = request.GetType().ToString().Split('.').Last();
            
            if (requiredPermissions.Any(reqPermission => !userPermissions.Contains(reqPermission)))
            {
                throw new ForbiddenAccessException($"User does not have required permissions for {requestName} request");
            }
        }
        
        return await next();
    }
}