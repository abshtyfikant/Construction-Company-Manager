using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repository
{
    public interface IReportRepository
    {
        void DeleteReport(int reportId);
        int AddReport(Report report);
        IQueryable<Report> GetAllReports();
        Report GetReport(int reportId);
        void UpdateReport(Report report);
    }
}
