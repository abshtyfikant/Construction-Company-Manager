using Application.DTO.Client;
using Application.Mapping;
using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Specialization
{
    public class NewSpecializationDto : IMapFrom<Domain.Model.Specialization>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static void Mapping(Profile profile)
        {
            profile.CreateMap<NewSpecializationDto, Domain.Model.Specialization>().ReverseMap();
        }
    }

    public class NewSpecializationValidation : AbstractValidator<NewSpecializationDto>
    {
        public NewSpecializationValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        }
    }
}
