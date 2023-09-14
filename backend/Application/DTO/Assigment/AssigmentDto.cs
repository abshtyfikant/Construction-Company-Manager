using Application.Mapping;
using AutoMapper;

namespace Application.DTO.Assigment;

public class AssigmentDto : IMapFrom<Domain.Model.Assignment>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string Employee { get; set; }
    public double RatePerHour { get; set; }
    public int ServiceId { get; set; }
    public string Service { get; set; }
    public string Function { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public static void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Model.Assignment, AssigmentDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id) )
            .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => s.EmployeeId))
            .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.Employee.FirstName + " " + s.Employee.LastName))
            .ForMember(d => d.RatePerHour, opt => opt.MapFrom(s => s.Employee.RatePerHour))
            .ForMember(d => d.ServiceId, opt => opt.MapFrom(s => s.ServiceId))
            .ForMember(d => d.Service, opt => opt.MapFrom(s => s.Service.ServiceType))
            .ForMember(d => d.Function, opt => opt.MapFrom(s => s.Function))
            .ForMember(d => d.StartDate, opt => opt.MapFrom(s => s.StartDate))
            .ForMember(d => d.EndDate, opt => opt.MapFrom(s => s.EndDate));
    }
}