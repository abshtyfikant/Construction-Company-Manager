using Application.DTO.Report;

namespace Application.Interfaces.Reports;

public interface IReportService
{
    int AddReport(NewReportDto report);
    List<ReportForListDto> GetReportsForList();
    object GetReport(int reportId);
    object UpdateReport(NewReportDto newReport);
    void DeleteReport(int reportId);
}