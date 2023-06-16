using AutoMapper;
using FluentValidation;

namespace Application.DTO.ResourceAllocation;

public class UpdateResourceAllocationDto
{
    public int Id { get; set; }
    public double AllocatedQuantity { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime EndDate { get; set; }

    public static void Mapping(Profile profile)
    {
        profile.CreateMap<NewResourceAllocationDto, Domain.Model.ServiceResource>().ReverseMap();
    }
}

public class UpdateResourceAllocationValidation : AbstractValidator<UpdateResourceAllocationDto>
{
    public UpdateResourceAllocationValidation()
    {
        RuleFor(x => x.AllocatedQuantity).NotEmpty();
        RuleFor(x => x.BeginDate).NotEmpty();
        RuleFor(x => x.EndDate).NotEmpty();
    }
}