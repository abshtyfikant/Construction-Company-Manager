using Application.Mapping;
using AutoMapper;

namespace Application.DTO.Resource;

public class ResourceDto : IMapFrom<Domain.Model.Resource>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Quantity { get; set; }

    public static void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Model.Resource, ResourceDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
            .ForMember(d => d.Quantity, opt => opt.MapFrom(s => s.Quantity));
    }
}