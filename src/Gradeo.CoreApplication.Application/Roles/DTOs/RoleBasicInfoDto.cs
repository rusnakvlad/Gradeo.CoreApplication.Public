using Gradeo.CoreApplication.Application.Common.Mappings;
using Gradeo.CoreApplication.Domain.Entities;

namespace Gradeo.CoreApplication.Application.Roles.DTOs;

public class RoleBasicInfoDto : IMapFrom<Role>
{
    public int Id { get; set; }
    public string RoleName { get; set; }
}