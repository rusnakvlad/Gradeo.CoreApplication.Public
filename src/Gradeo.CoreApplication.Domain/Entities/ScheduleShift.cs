using Gradeo.CoreApplication.Domain.Interfaces;

namespace Gradeo.CoreApplication.Domain.Entities;

public class ScheduleShift : ISoftDeletable, IBusinessUnitBased
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public int BusinessUnitId { get; set; }
    
    public BusinessUnit BusinessUnit { get; set; }
    public bool IsDeleted { get; set; }
}