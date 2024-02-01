using AutoMapper;
using Gradeo.CoreApplication.Application.Permissions.DTOs;
using Gradeo.CoreApplication.Application.Roles.DTOs;
using Gradeo.CoreApplication.Domain.Entities;
using Gradeo.CoreApplication.Domain.Enums;

namespace Gradeo.CoreApplication.Application.Roles;

public class RoleMappingProfile : Profile
{
    public RoleMappingProfile()
    {
        CreateMap<Role, RoleDto>()
            .ForMember(x => x.Permissions, opt => opt.MapFrom(src => src.Permissions.Select(rp => new PermissionDto()
            {
                PermissionId = (Permission)rp.PermissionId
            })));
    }
}