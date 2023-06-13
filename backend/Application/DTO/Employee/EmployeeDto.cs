using Application.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Employee
{
    public class EmployeeDto : IMapFrom<Domain.Model.Employee>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public double RatePerHour { get; set; }
        public string MainSpecialization { get; set; }

        public static void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Model.Employee, EmployeeDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
                .ForMember(d => d.City, opt => opt.MapFrom(s => s.City))
                .ForMember(d => d.RatePerHour, opt => opt.MapFrom(s => s.RatePerHour))
                .ForMember(d => d.MainSpecialization, opt => opt.MapFrom(s => s.MainSpecialization.Name));
        }   
    }
}
