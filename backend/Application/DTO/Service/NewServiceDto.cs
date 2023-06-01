using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Service
{
    public class NewServiceDto
    {
        public int Id { get; set; }
        public string ServiceType { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string City { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewServiceDto, Domain.Model.Service>().ReverseMap();
        }
    }

    public class NewServiceValidation : AbstractValidator<NewServiceDto>
    {
        public NewServiceValidation()
        {
            RuleFor(x => x.ServiceType).NotEmpty().MaximumLength(255);
            RuleFor(x => x.BeginDate).NotEmpty();
            RuleFor(x => x.EndDate).NotEmpty();
            RuleFor(x => x.City).NotEmpty().MaximumLength(255);
        }
    }
}
