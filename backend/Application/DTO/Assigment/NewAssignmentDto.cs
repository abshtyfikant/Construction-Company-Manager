using Application.Mapping;
using AutoMapper;
using FluentValidation;

namespace Application.DTO.Assigment;

public class NewAssignmentDto : IMapFrom<Domain.Model.Assignment>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int ServiceId { get; set; }
    public string Function { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public static void Mapping(Profile profile)
    {
        profile.CreateMap<NewAssignmentDto, Domain.Model.Assignment>().ReverseMap();
    }
}

public class NewAssignmentValidation : AbstractValidator<NewAssignmentDto>
{
    public NewAssignmentValidation()
    {
        RuleFor(x => x.EmployeeId).NotEmpty();
        RuleFor(x => x.ServiceId).NotEmpty();
        RuleFor(x => x.Function).NotEmpty().MaximumLength(50);
        RuleFor(x => x.StartDate).NotEmpty();
        RuleFor(x => x.EndDate).NotEmpty();
    }
}