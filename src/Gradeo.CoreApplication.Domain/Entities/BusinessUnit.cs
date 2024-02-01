using Gradeo.CoreApplication.Domain.Enums;
using Gradeo.CoreApplication.Domain.Interfaces;

namespace Gradeo.CoreApplication.Domain.Entities;

public class BusinessUnit : BaseEntity, IEntityIdentity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public BusinessUnitType BusinessUnitType { get; set; }
    public string? CosmosDbConnection { get; set; }
}