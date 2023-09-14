using Domain.Interfaces.Repository;
using Domain.Model;

namespace Infrastructure.Repositories; 

internal class ReportRepository : IReportRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ReportRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public int AddReport(Report report)
    {
        //if (!_dbContext.Services.Any(s => s.Id == report.ServiceId)) throw new Exception("Service not found");
        _dbContext.Reports.Add(report);
        _dbContext.SaveChanges();
        return report.Id;
    }

    public void DeleteReport(int reportId)
    {
        var report = _dbContext.Reports.Find(reportId);
        if (report is not null)
        {
            _dbContext.Reports.Remove(report);
            _dbContext.SaveChanges();
        }
    }

    public IQueryable<Report> GetAllReports()
    {
        var reports = _dbContext.Reports;
        return reports;
    }

    public Report GetReport(int reportId)
    {
        var report = _dbContext.Reports.FirstOrDefault(i => i.Id == reportId);
        return report;
    }

    public void UpdateReport(Report report)
    {
        _dbContext.Attach(report);
        _dbContext.Entry(report).Property("ReportType").IsModified = true;
        _dbContext.Entry(report).Property("Description").IsModified = true;
        _dbContext.Entry(report).Property("BeginDate").IsModified = true;
        _dbContext.Entry(report).Property("EndDate").IsModified = true;
        _dbContext.Entry(report).Property("Amount").IsModified = true;
        _dbContext.Entry(report).Property("City").IsModified = true;
        _dbContext.SaveChanges();
    }
}