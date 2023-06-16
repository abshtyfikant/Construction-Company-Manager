using Application.Mapping;
using AutoMapper;
using FluentValidation;

namespace Application.DTO.ResourceAllocation;

public class NewResourceAllocationDto : IMapFrom<Domain.Model.ServiceResource>
{
    public int Id { get; set; }
    public int ServiceId { get; set; }
    public int ResourceId { get; set; }
    public double AllocatedQuantity { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime EndDate { get; set; }

    public static void Mapping(Profile profile)
    {
        profile.CreateMap<NewResourceAllocationDto, Domain.Model.ServiceResource>().ReverseMap();
    }
}

public class NewResourceAllocationValidation : AbstractValidator<NewResourceAllocationDto>
{
    public NewResourceAllocationValidation()
    {
        RuleFor(x => x.ServiceId).NotEmpty();
        RuleFor(x => x.ResourceId).NotEmpty();
        RuleFor(x => x.AllocatedQuantity).NotEmpty();
        RuleFor(x => x.BeginDate).NotEmpty();
        RuleFor(x => x.EndDate).NotEmpty();
    }
}