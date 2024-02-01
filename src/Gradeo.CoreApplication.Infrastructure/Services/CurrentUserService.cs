using System.Security.Claims;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Application.Users.DTOs;
using Gradeo.CoreApplication.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Gradeo.CoreApplication.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;
    
    public CurrentUserService(IHttpContextAccessor httpContextAccessor, IApplicationDbContext applicationDbContext, IMapper mapper, ICacheService cacheService)
    {
        _httpContextAccessor = httpContextAccessor;
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
        _cacheService = cacheService;
    }
    
    public Guid? UserId
    {
        get
        {
            if (Guid.TryParse(_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier), out var guid)) {
                return guid;
            } 
            else
            {
                return null;
            }
        }
    }

    public UserType? UserType => _applicationDbContext.Users.First(x => x.Id == UserId).UserType;

    public UserDetailsDto UserDetails =>
        _cacheService.GetOrCreateUserScopeAsync(UserId.Value.ToString(), GetUserScopeDetails, UserId.Value);

    public IEnumerable<Permission> GetCurrentUserPermissions()
    {
        return _applicationDbContext.UserRoles.AsNoTracking()
            .Where(x => x.UserId == UserId)
            .Include(x => x.Role)
            .ThenInclude(x => x.Permissions)
            .SelectMany(x => x.Role.Permissions.Select(rp => rp.PermissionId))
            .ToList()
            .Select(x => (Permission)x);
    }

    public int? GetUserBusinessUnitId()
    {
        return _applicationDbContext.Users.AsNoTracking().FirstOrDefault(x => x.Id == UserId)?.BusinessUnitId;
    }

    private UserDetailsDto GetUserScopeDetails(Guid userId)
    {
        return _applicationDbContext.Users.AsNoTracking().Include(x => x.UserRoles).ThenInclude(x => x.Role)
            .ThenInclude(x => x.Permissions).ProjectTo<UserDetailsDto>(_mapper.ConfigurationProvider).First(x => x.Id == userId);
    }
}