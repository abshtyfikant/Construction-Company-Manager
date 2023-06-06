﻿using Domain.Interfaces.Repository;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    internal class ReportRepository : IReportRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ReportRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int AddReport(Report report)
        {
            _dbContext.Reports.Add(report);
            _dbContext.SaveChanges();
            return report.Id;
        }

        public void DeleteReport(int reportId)
        {
            var report = _dbContext.Reports.Find(reportId);
            if (report != null)
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
            throw new NotImplementedException();
        }
    }
}
