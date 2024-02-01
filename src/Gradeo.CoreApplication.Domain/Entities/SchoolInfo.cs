using Gradeo.CoreApplication.Domain.Interfaces;

namespace Gradeo.CoreApplication.Domain.Entities;

public class SchoolInfo : BaseEntity, IEntityIdentity, ISoftDeletable
{
    public int Id { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public bool IsDeleted { get; set; }

    public BusinessUnit BusinessUnit { get; set; }
}
