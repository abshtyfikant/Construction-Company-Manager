using Application.Mapping;
using AutoMapper;
using FluentValidation;

namespace Application.DTO.Resource;

public class NewResourceDto : IMapFrom<Domain.Model.Resource>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Quantity { get; set; }

    public static void Mapping(Profile profile)
    {
        profile.CreateMap<NewResourceDto, Domain.Model.Resource>().ReverseMap();
    }
}

public class AddResourceValidation : AbstractValidator<NewResourceDto>
{
    public AddResourceValidation()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Quantity).NotEmpty();
    }
}