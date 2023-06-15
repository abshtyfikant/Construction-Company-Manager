using Application.Mapping;
using AutoMapper;
using FluentValidation;

namespace Application.DTO.Comment;

public class NewCommentDto : IMapFrom<Domain.Model.Comment>
{
    public int Id { get; set; }
    public int ServiceId { get; set; }
    public Guid UserId { get; set; }
    public string Content { get; set; }
    public DateTime Date { get; set; }

    public static void Mapping(Profile profile)
    {
        profile.CreateMap<NewCommentDto, Domain.Model.Comment>().ReverseMap();
    }
}

public class NewCommentValidation : AbstractValidator<NewCommentDto>
{
    public NewCommentValidation()
    {
        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage("Content is required")
            .MaximumLength(128);
        RuleFor(x => x.ServiceId)
            .NotEmpty()
            .WithMessage("ServiceId is required");
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId is required");
        RuleFor(x => x.Date)
            .NotEmpty()
            .WithMessage("Date is required");
    }
}