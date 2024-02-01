using AutoMapper;
using Gradeo.CoreApplication.Application.Common.Extensions;
using Gradeo.CoreApplication.Application.Users.DTOs;
using Gradeo.CoreApplication.Domain.Entities;
using Gradeo.CoreApplication.Domain.Enums;

namespace Gradeo.CoreApplication.Application.Users;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(x => x.UserType, opt => opt.MapFrom(x => x.UserType.HasValue ? x.UserType.ToString() : null))
            .ForMember(x => x.RoleIds, opt => opt.MapFrom(x => x.UserRoles.Select(x => x.RoleId)));
        
        CreateMap<UserDto, User>()
            .ForMember(x => x.UserType, opt => opt.MapFrom(x => x.UserType.IsNullOrEmpty() ? null : Enum.Parse(typeof(UserType), x.UserType)));

        CreateMap<User, UserDetailsDto>()
            .ForMember(x => x.Permissions,
                opt => opt.MapFrom(x =>
                    x.UserRoles.SelectMany(ur => ur.Role.Permissions.Select(rp => rp.PermissionId))));

    }
}