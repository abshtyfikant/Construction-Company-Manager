using Application.Mapping;
using AutoMapper;

namespace Application.DTO.Material;

public class MaterialDto : IMapFrom<Domain.Model.Material>
{
    public int Id { get; set; }
    public int ServiceId { get; set; }
    public string Name { get; set; }
    public string unit { get; set; }
    public decimal Price { get; set; }
    public double Quantity { get; set; }

    public static void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Model.Material, MaterialDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.ServiceId, opt => opt.MapFrom(s => s.ServiceId))
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
            .ForMember(d => d.unit, opt => opt.MapFrom(s => s.Unit))
            .ForMember(d => d.Price, opt => opt.MapFrom(s => s.Price))
            .ForMember(d => d.Quantity, opt => opt.MapFrom(s => s.Quantity));
    }
}