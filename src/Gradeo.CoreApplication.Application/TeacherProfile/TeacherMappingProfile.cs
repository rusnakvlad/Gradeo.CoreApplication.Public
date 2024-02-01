using AutoMapper;
using Gradeo.CoreApplication.Application.MasterSubjects.DTOs;
using Gradeo.CoreApplication.Application.StudyGroups.DTOs;
using Gradeo.CoreApplication.Application.TeacherProfile.DTOs;

namespace Gradeo.CoreApplication.Application.TeacherProfile;

public class TeacherMappingProfile : Profile
{
    public TeacherMappingProfile()
    {
        CreateMap<Domain.Entities.TeacherProfile, TeacherProfileDto>()
            .ForMember(x => x.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(x => x.LastName, opt => opt.MapFrom(src => src.User.LastName))
            .ForMember(x => x.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(x => x.StudyGroups,
                opt => opt.MapFrom(src => src.StudyGroups.Select(sg => new StudyGroupBasicInfoDto()
                    { Id = sg.StudyGroupId, Name = sg.StudyGroup.Name })))
            .ForMember(x => x.Subjects,
                opt => opt.MapFrom(src => src.AssignedSubjects.Select(s => new MasterSubjectDto()
                    { Id = s.MasterSubjectId, Name = s.MasterSubject.Name })));
    }
}