using Gradeo.CoreApplication.Application.Common.Mappings;
using Gradeo.CoreApplication.Domain.Entities;

namespace Gradeo.CoreApplication.Application.MasterSubjects.DTOs;

public class MasterSubjectDto : IMapFrom<MasterSubject>
{
    public int Id { get; set; }
    public string Name { get; set; }
}