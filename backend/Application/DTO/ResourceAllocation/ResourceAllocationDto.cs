using Application.Mapping;
using AutoMapper;

namespace Application.DTO.ResourceAllocation;

public class ResourceAllocationDto : IMapFrom<Domain.Model.ServiceResource>
{
    public int Id { get; set; }
    public int ServiceId { get; set; }
    public int ResourceId { get; set; }
    public string ResourceName { get; set; }
    public double AllocatedQuantity { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime EndDate { get; set; }

    public static void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Model.ServiceResource, ResourceAllocationDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.ServiceId, opt => opt.MapFrom(s => s.ServiceId))
            .ForMember(d => d.ResourceId, opt => opt.MapFrom(s => s.ResourceId))
            .ForMember(d => d.ResourceName, opt => opt.MapFrom(s => s.Resource.Name))
            .ForMember(d => d.AllocatedQuantity, opt => opt.MapFrom(s => s.AllocatedQuantity))
            .ForMember(d => d.BeginDate, opt => opt.MapFrom(s => s.BeginDate))
            .ForMember(d => d.EndDate, opt => opt.MapFrom(s => s.EndDate));
    }
}