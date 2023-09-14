using Application.Mapping;
using AutoMapper;

namespace Application.DTO.Report;

public class ReportDetailsDto : IMapFrom<Domain.Model.Report>
{
    public int Id { get; set; }
    public int ServiceId { get; set; }
    public int EmployeeId { get; set; }
    public string ReportType { get; set; }
    public string Description { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Amount { get; set; }
    public string City { get; set; }
    public string Author { get; set; }

    public static void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Model.Report, ReportDetailsDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.ServiceId, opt => opt.MapFrom(s => s.ServiceId))
            .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => s.EmployeeId))
            .ForMember(d => d.ReportType, opt => opt.MapFrom(s => s.ReportType))
            .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
            .ForMember(d => d.BeginDate, opt => opt.MapFrom(s => s.BeginDate))
            .ForMember(d => d.EndDate, opt => opt.MapFrom(s => s.EndDate))
            .ForMember(d => d.Amount, opt => opt.MapFrom(s => s.Amount))
            .ForMember(d => d.City, opt => opt.MapFrom(s => s.City))
            .ForMember(d => d.Author, opt => opt.MapFrom(s => s.User.FirstName + " " + s.User.LastName));
    }
}