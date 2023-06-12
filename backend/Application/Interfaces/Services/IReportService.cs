using Application.DTO.Report;
using Application.DTO.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Reports
{
    public interface IReportService
    {
        int AddReport(NewReportDto report);
        List<ReportForListDto> GetReportsForList();
        object GetReport(int reportId);
        object UpdateReport(NewReportDto newReport);
        void DeleteReport(int reportId);
    }
}
