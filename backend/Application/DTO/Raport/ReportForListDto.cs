using Application.DTO.Service;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Raport
{
    public class ReportForListDto
    {
        public int Id { get; set; }
        public string ReportType { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Model.Report, ReportForListDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.ReportType, opt => opt.MapFrom(s => s.ReportType))
                .ForMember(d => d.BeginDate, opt => opt.MapFrom(s => s.BeginDate))
                .ForMember(d => d.EndDate, opt => opt.MapFrom(s => s.EndDate))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description));
        }
    }
}
