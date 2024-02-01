using Gradeo.CoreApplication.Application.Common.Attributes;
using Gradeo.CoreApplication.Application.Common.Interfaces;
using Gradeo.CoreApplication.Domain.Entities;
using Gradeo.CoreApplication.Domain.Enums;
using MediatR;

namespace Gradeo.CoreApplication.Application.Schools.Commands;

[Authorize(Permission.CanCreateSchools)]
public class DeleteSchoolsCommand : IRequest
{
    public List<int> ids { get; set; }
}

public class DeleteSchoolsCommandHandler : IRequestHandler<DeleteSchoolsCommand>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public DeleteSchoolsCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Unit> Handle(DeleteSchoolsCommand request, CancellationToken cancellationToken)
    {
        _applicationDbContext.BusinessUnits.RemoveRange(request.ids.Select(x => new BusinessUnit(){Id = x}));
        _applicationDbContext.SchoolsInfo.RemoveRange(request.ids.Select(x => new SchoolInfo(){Id = x}));
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}