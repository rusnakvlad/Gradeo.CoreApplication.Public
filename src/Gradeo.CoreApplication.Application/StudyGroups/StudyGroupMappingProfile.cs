using AutoMapper;
using Gradeo.CoreApplication.Application.MasterSubjects.DTOs;
using Gradeo.CoreApplication.Application.StudyGroups.DTOs;
using Gradeo.CoreApplication.Domain.Entities;

namespace Gradeo.CoreApplication.Application.StudyGroups;

public class StudyGroupMappingProfile : Profile
{
    public StudyGroupMappingProfile()
    {
        CreateMap<StudyGroup, StudyGroupDto>()
            .ForMember(x => x.Subjects,
                opt => opt.MapFrom(src => src.StudyGroupSubjects.Select(s => new MasterSubjectDto()
                    { Id = s.MasterSubjectId, Name = s.MasterSubject.Name })));
    }
    
}