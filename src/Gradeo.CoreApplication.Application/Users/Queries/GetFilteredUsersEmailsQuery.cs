using Gradeo.CoreApplication.Application.Common.Attributes;
using Gradeo.CoreApplication.Application.Common.Extensions;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gradeo.CoreApplication.Application.Users.Queries;

[Authorize(Permission.CanViewUsers)]
public class GetFilteredUsersEmailsQuery : IRequest<IEnumerable<string>>
{
    public string? SearchTerm { get; set; }
    public UserType? UserType { get; set; }
}


public class GetFilteredUsersEmailsQueryHandler : IRequestHandler<GetFilteredUsersEmailsQuery, IEnumerable<string>>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ICurrentUserService _currentUserService;
    
    public GetFilteredUsersEmailsQueryHandler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService)
    {
        _applicationDbContext = applicationDbContext;
        _currentUserService = currentUserService;
    }
    
    public async Task<IEnumerable<string>> Handle(GetFilteredUsersEmailsQuery request, CancellationToken cancellationToken)
    {
        var currentUserSchoolId = _currentUserService.GetUserBusinessUnitId();
        var usersWithUsedEmail = request.UserType is UserType.Student
            ? await _applicationDbContext.StudentProfiles.Include(x => x.User).Select(x => x.User.Id)
                .ToListAsync(cancellationToken)
            : await _applicationDbContext.TeacherProfiles.Include(x => x.User).Select(x => x.User.Id)
                .ToListAsync(cancellationToken);
        
        return await _applicationDbContext.Users.AsNoTracking()
            .Where(x => x.BusinessUnitId == currentUserSchoolId)
            .WhereIf(request.UserType != null, x => x.UserType == request.UserType && !usersWithUsedEmail.Contains(x.Id))
            .Select(x => x.Email)
            .ToListAsync(cancellationToken);
    }
}

