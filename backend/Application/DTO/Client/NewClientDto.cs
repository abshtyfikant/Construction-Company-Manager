using Application.Mapping;
using AutoMapper;
using FluentValidation;

namespace Application.DTO.Client;

public class NewClientDto : IMapFrom<Domain.Model.Client>
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string City { get; set; }

    public static void Mapping(Profile profile)
    {
        profile.CreateMap<NewClientDto, Domain.Model.Client>().ReverseMap();
    }
}

public class NewClientValidation : AbstractValidator<NewClientDto>
{
    public NewClientValidation()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.City).NotEmpty().MaximumLength(50);
    }
}