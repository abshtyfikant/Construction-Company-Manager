using Application.Mapping;
using AutoMapper;

namespace Application.DTO.Service;

public class ServiceDetailsDto : IMapFrom<Domain.Model.Service>
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public string ServiceType { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime EndDate { get; set; }
    public string City { get; set; }
    public string ServiceStatus { get; set; }
    public string PaymentStatus { get; set; }
    public decimal Price { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Model.Service, ServiceDetailsDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.ClientId, opt => opt.MapFrom(s => s.ClientId))
            .ForMember(d => d.ServiceType, opt => opt.MapFrom(s => s.ServiceType))
            .ForMember(d => d.BeginDate, opt => opt.MapFrom(s => s.BeginDate))
            .ForMember(d => d.EndDate, opt => opt.MapFrom(s => s.EndDate))
            .ForMember(d => d.City, opt => opt.MapFrom(s => s.City))
            .ForMember(d => d.ServiceStatus, opt => opt.MapFrom(s => s.ServiceStatus))
            .ForMember(d => d.PaymentStatus, opt => opt.MapFrom(s => s.PaymentStatus))
            .ForMember(d => d.Price, opt => opt.MapFrom(s => s.Price));
    }
}