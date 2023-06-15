using Domain.Model;

namespace Domain.Interfaces.Repository;

public interface IReportRepository
{
    void DeleteReport(int reportId);
    int AddReport(Report report);
    IQueryable<Report> GetAllReports();
    Report GetReport(int reportId);
    void UpdateReport(Report report);
}