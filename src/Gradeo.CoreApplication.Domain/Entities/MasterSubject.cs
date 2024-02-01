using Gradeo.CoreApplication.Domain.Interfaces;

namespace Gradeo.CoreApplication.Domain.Entities;

public class MasterSubject : BaseEntity, IEntityIdentity, ISoftDeletable
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsDeleted { get; set; }
    public int? BusinessUnitId { get; set; }
    public BusinessUnit? BusinessUnit { get; set; }
}