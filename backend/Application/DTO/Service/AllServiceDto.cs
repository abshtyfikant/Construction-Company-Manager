using Application.DTO.Assigment;
using Application.DTO.Material;
using Application.DTO.ResourceAllocation;
using Application.Mapping;
using AutoMapper;
using Domain.Model;
using FluentValidation;


namespace Application.DTO.Service;

public class AllServiceDto : IMapFrom<Domain.Model.Service>
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public string ServiceType { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime EndDate { get; set; }
    public string ServiceStatus { get; set; }
    public string PaymentStatus { get; set; }
    public string City { get; set; }
    public decimal Price { get; set; }
    public List<NewMaterialDto> Materials { get; set; }
    public List<NewResourceAllocationDto> Resources { get; set; }
    public List<NewAssignmentDto> Assigments { get; set; }

    public static void Mapping(Profile profile)
    {
        profile.CreateMap<AllServiceDto, Domain.Model.Service>().ReverseMap();
    }
}

public class AllServiceDtoValidation : AbstractValidator<AllServiceDto>
{
    public AllServiceDtoValidation()
    {
        RuleFor(x => x.ServiceType).NotEmpty().MaximumLength(255);
        RuleFor(x => x.ClientId).NotEmpty();
        RuleFor(x => x.BeginDate).NotEmpty();
        RuleFor(x => x.EndDate).NotEmpty();
        RuleFor(x => x.ServiceStatus).NotEmpty();
        RuleFor(x => x.PaymentStatus).NotEmpty();
        RuleFor(x => x.City).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Price).NotEmpty();
        RuleFor(x => x.Materials).NotEmpty();
        RuleFor(x => x.Resources).NotEmpty();
        RuleFor(x => x.Assigments).NotEmpty();
    }
}