﻿using Application.DTO.Service;
using Application.Mapping;
using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Report
{
    public class NewReportDto : IMapFrom<Domain.Model.Report>
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public string ReportType { get; set; }
        public string Description { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Amount { get; set; }
        public string City { get; set; }

        public static void Mapping(Profile profile)
        {
            profile.CreateMap<NewReportDto, Domain.Model.Report>().ReverseMap();
        }
    }

    public class NewReportValidation : AbstractValidator<NewReportDto>
    {
        public NewReportValidation()
        {
            RuleFor(x => x.ServiceId).NotEmpty();
            RuleFor(x => x.ReportType).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(255);
            RuleFor(x => x.BeginDate).NotEmpty();
            RuleFor(x => x.EndDate).NotEmpty();
            RuleFor(x => x.Amount).NotEmpty();
            RuleFor(x => x.City).NotEmpty().MaximumLength(50);
            RuleFor(x => x.City).NotEmpty().MaximumLength(255);
        }
    }
}
