using Gradeo.CoreApplication.Application.Common.Extensions;
using Gradeo.CoreApplication.Application.Common.Helpers;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Domain.Entities;
using Gradeo.CoreApplication.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gradeo.CoreApplication.Application.Schools.Commands;

public class UpsertSchoolCommand : IRequest
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
}

public class UpsertSchoolCommandHandler : IRequestHandler<UpsertSchoolCommand>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ICurrentUserService _currentUserService;
    
    public UpsertSchoolCommandHandler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService)
    {
        _applicationDbContext = applicationDbContext;
        _currentUserService = currentUserService;
    }
    
    public async Task<Unit> Handle(UpsertSchoolCommand request, CancellationToken cancellationToken)
    {
        if (request.Id.IsNullOrZero())
        {
            PermissionHelper.ValidatePermissions(_currentUserService.GetCurrentUserPermissions(), Permission.CanCreateSchools);
            var businessUnit = new BusinessUnit()
            {
                Name = request.Name,
                BusinessUnitType = BusinessUnitType.School,
            };

            var school = new SchoolInfo()
            {
                Country = request.Country,
                City = request.City,
                BusinessUnit = businessUnit
            };

            _applicationDbContext.SchoolsInfo.Add(school);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
        PermissionHelper.ValidatePermissions(_currentUserService.GetCurrentUserPermissions(), Permission.CanEditSchools);

        var schoolEntity = await _applicationDbContext.SchoolsInfo
            .Include(x => x.BusinessUnit)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (schoolEntity == null)
        {
            throw new ArgumentException($"School with id ({request.Id}) was not found");
        }

        schoolEntity.BusinessUnit.Name = request.Name;
        schoolEntity.Country = request.Country;
        schoolEntity.City = request.City;
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
