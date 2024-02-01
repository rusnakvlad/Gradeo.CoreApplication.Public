using AutoMapper;
using Gradeo.CoreApplication.Application.StudentProfile.DTOs;
using Gradeo.CoreApplication.Application.StudyGroups.DTOs;

namespace Gradeo.CoreApplication.Application.StudentProfile;

public class StudentMappingProfile : Profile
{
    public StudentMappingProfile()
    {
        CreateMap<Domain.Entities.StudentProfile, StudentProfileDto>()
            .ForMember(x => x.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(x => x.LastName, opt => opt.MapFrom(src => src.User.LastName))
            .ForMember(x => x.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(x => x.StudyGroups,
                opt => opt.MapFrom(src => src.StudyGroups.Select(sg => new StudyGroupBasicInfoDto()
                    { Id = sg.StudyGroupId, Name = sg.StudyGroup.Name })));

    }
    
}