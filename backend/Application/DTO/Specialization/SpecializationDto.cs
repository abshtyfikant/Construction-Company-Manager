using Application.Mapping;
using AutoMapper;

namespace Application.DTO.Specialization;

public class SpecializationDto : IMapFrom<Domain.Model.Specialization>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Model.Specialization, SpecializationDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name));
    }
}