using Application.Mapping;
using AutoMapper;

namespace Application.DTO.Client;

public class ClientDto : IMapFrom<Domain.Model.Client>
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string City { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Model.Client, ClientDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
            .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
            .ForMember(d => d.City, opt => opt.MapFrom(s => s.City));
    }
}