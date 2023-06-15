using Application.DTO.Employee;
using Application.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Assigment
{
    public class AssigmentDto : IMapFrom<Domain.Model.Assigment>
    {
        public int EmployeeId { get; set; }
        public string Employee { get; set; }
        public int ServiceId { get; set; }
        public string Service { get; set; }
        public string Function { get; set; }

        public static void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Model.Assigment, AssigmentDto>()
                .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => s.EmployeeId))
                .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee.FirstName + " " + s.Employee.LastName))
                .ForMember(d => d.ServiceId, opt => opt.MapFrom(s => s.ServiceId))
                .ForMember(d => d.Service, opt => opt.MapFrom(s => s.Service.ServiceType))
                .ForMember(d => d.Function, opt => opt.MapFrom(s => s.Function));
        }
    }


}
