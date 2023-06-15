using Application.Mapping;
using AutoMapper;
using FluentValidation;

namespace Application.DTO.Material;

public class NewMaterialDto : IMapFrom<Domain.Model.Material>
{
    public int Id { get; set; }
    public int ServiceId { get; set; }
    public string Name { get; set; }
    public string unit { get; set; }
    public decimal Price { get; set; }
    public double Quantity { get; set; }

    public static void Mapping(Profile profile)
    {
        profile.CreateMap<NewMaterialDto, Domain.Model.Material>().ReverseMap();
    }
}

public class NewMaterialValidation : AbstractValidator<NewMaterialDto>
{
    public NewMaterialValidation()
    {
        RuleFor(x => x.ServiceId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.unit).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Price).NotEmpty();
        RuleFor(x => x.Quantity).NotEmpty();
    }
}