using Gradeo.CoreApplication.Application.Common.Mappings;
using Gradeo.CoreApplication.Domain.Entities;

namespace Gradeo.CoreApplication.Application.Common.DTOs;

public class BusinessUnitDto : IMapFrom<BusinessUnit>
{
    public int Id { get; set; }
    public string Name { get; set; }
}