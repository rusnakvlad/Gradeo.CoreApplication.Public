using Gradeo.CoreApplication.Domain.Entities;

namespace Gradeo.CoreApplication.Domain.Interfaces;

public interface IBusinessUnitBased
{
    public int BusinessUnitId { get; set; }
    public BusinessUnit BusinessUnit { get; set; }
}