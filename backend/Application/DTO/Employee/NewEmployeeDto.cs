using Application.Mapping;
using AutoMapper;
using FluentValidation;

namespace Application.DTO.Employee;

public class NewEmployeeDto : IMapFrom<Domain.Model.Employee>
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string City { get; set; }
    public double RatePerHour { get; set; }
    public int MainSpecializationId { get; set; }

    public static void Mapping(Profile profile)
    {
        profile.CreateMap<NewEmployeeDto, Domain.Model.Employee>().ReverseMap();
    }
}

public class NewEmployeeValidation : AbstractValidator<NewEmployeeDto>
{
    public NewEmployeeValidation()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(255);
        RuleFor(x => x.City).NotEmpty().MaximumLength(255);
        RuleFor(x => x.RatePerHour).NotEmpty();
        RuleFor(x => x.MainSpecializationId).NotEmpty();
    }
}